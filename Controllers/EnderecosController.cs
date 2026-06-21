using AEC.Data;
using AEC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Text;

namespace AEC.Controllers
{
    public class EnderecosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public EnderecosController(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.UsuarioNome = HttpContext.Session.GetString("UsuarioNome");

            var enderecos = await _context.Enderecos
                .Where(e => e.UsuarioId == usuarioId)
                .ToListAsync();

            return View(enderecos);
        }

        public IActionResult Criar()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(Endereco endereco)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (ModelState.IsValid)
            {
                endereco.UsuarioId = usuarioId.Value;

                _context.Enderecos.Add(endereco);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(endereco);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var endereco = await _context.Enderecos
                .FirstOrDefaultAsync(e => e.Id == id && e.UsuarioId == usuarioId);

            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Endereco endereco)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (ModelState.IsValid)
            {
                endereco.UsuarioId = usuarioId.Value;

                _context.Enderecos.Update(endereco);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(endereco);
        }

        public async Task<IActionResult> Excluir(int id)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var endereco = await _context.Enderecos
                .FirstOrDefaultAsync(e => e.Id == id && e.UsuarioId == usuarioId);

            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmarExclusao(int id)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var endereco = await _context.Enderecos
                .FirstOrDefaultAsync(e => e.Id == id && e.UsuarioId == usuarioId);

            if (endereco != null)
            {
                _context.Enderecos.Remove(endereco);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> BuscarCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
            {
                return BadRequest(new { erro = "Informe um CEP válido." });
            }

            cep = new string(cep.Where(char.IsDigit).ToArray());

            if (cep.Length != 8)
            {
                return BadRequest(new { erro = "O CEP deve conter 8 dígitos." });
            }

            var client = _httpClientFactory.CreateClient();

            var resultado = await client.GetFromJsonAsync<ViaCepResposta>(
                $"https://viacep.com.br/ws/{cep}/json/"
            );

            if (resultado == null || resultado.Erro)
            {
                return NotFound(new { erro = "CEP não encontrado." });
            }

            return Json(new
            {
                cep = resultado.Cep,
                logradouro = resultado.Logradouro,
                complemento = resultado.Complemento,
                bairro = resultado.Bairro,
                cidade = resultado.Cidade,
                localidade = resultado.Cidade,
                uf = resultado.Uf
            });
        }

        public async Task<IActionResult> ExportarCsv()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var enderecos = await _context.Enderecos
                .Where(e => e.UsuarioId == usuarioId)
                .ToListAsync();

            var csv = new StringBuilder();

            csv.AppendLine("Id;CEP;Logradouro;Numero;Complemento;Bairro;Cidade;UF");

            foreach (var endereco in enderecos)
            {
                csv.AppendLine(
                    $"{endereco.Id};" +
                    $"{TratarCampoCsv(endereco.Cep)};" +
                    $"{TratarCampoCsv(endereco.Logradouro)};" +
                    $"{TratarCampoCsv(endereco.Numero)};" +
                    $"{TratarCampoCsv(endereco.Complemento)};" +
                    $"{TratarCampoCsv(endereco.Bairro)};" +
                    $"{TratarCampoCsv(endereco.Cidade)};" +
                    $"{TratarCampoCsv(endereco.Uf)}"
                );
            }

            var bytes = Encoding.UTF8.GetPreamble()
                .Concat(Encoding.UTF8.GetBytes(csv.ToString()))
                .ToArray();

            return File(bytes, "text/csv", "enderecos.csv");
        }

        private string TratarCampoCsv(string? campo)
        {
            if (string.IsNullOrEmpty(campo))
            {
                return "";
            }

            campo = campo.Replace("\"", "\"\"");

            if (campo.Contains(";") || campo.Contains("\"") || campo.Contains("\n"))
            {
                return $"\"{campo}\"";
            }

            return campo;
        }
    }
}