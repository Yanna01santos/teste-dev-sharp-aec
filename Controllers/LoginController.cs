using AEC.Data;
using AEC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AEC.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuarioEncontrado = await _context.Usuarios
                .FirstOrDefaultAsync(u =>
                    u.UsuarioLogin == model.Usuario &&
                    u.Senha == model.Senha
                );

            if (usuarioEncontrado == null)
            {
                ViewBag.Erro = "Usuário ou senha inválidos.";
                return View(model);
            }

            HttpContext.Session.SetInt32("UsuarioId", usuarioEncontrado.Id);
            HttpContext.Session.SetString("UsuarioNome", usuarioEncontrado.Nome);

            return RedirectToAction("Index", "Enderecos");
        }

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
