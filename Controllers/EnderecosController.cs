using Microsoft.AspNetCore.Mvc;

namespace AEC.Controllers
{
    public class EnderecosController : Controller
    {
        public IActionResult Index()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.UsuarioNome = HttpContext.Session.GetString("UsuarioNome");

            return View();
        }
    }
}