using AEC.Data;
using AEC.Models;
using AEC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AEC.Controllers
{
    public class EnderecosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ViaCepService _viaCepService;
        private readonly CsvService _csvService;

        public EnderecosController(
            AppDbContext context,
            ViaCepService viaCepService,
            CsvService csvService)
        {
            _context = context;
            _viaCepService = viaCepService;
            _csvService = csvService;
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

            var resultado = await _viaCepService.BuscarEnderecoPorCepAsync(cep);

            if (resultado == null)
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

            var arquivo = _csvService.GerarCsvEnderecos(enderecos);

            return File(arquivo, "text/csv", "enderecos.csv");
        }
    }
}