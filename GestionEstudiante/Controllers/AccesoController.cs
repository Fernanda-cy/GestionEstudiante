using GestionEstudiante.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GestionEstudiante.Controllers
{
    public class AccesoController : Controller
    {
        private readonly EstudianteRepository repo;

        // 👇 CORRECCIÓN AQUÍ: Usa 'this.repo'
        public AccesoController(EstudianteRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string correo, string password)
        {
            // Ahora sí, 'repo' tendrá datos y funcionará
            var user = repo.ValidarUsuario(correo, password);

            if (user != null)
            {
                HttpContext.Session.SetString("UsuarioNombre", user.Nombre);
                HttpContext.Session.SetString("UsuarioRol", user.Rol);
                return RedirectToAction("Listado", "Estudiante");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View();
        }

        public IActionResult CerrarSesion()
        {
            // 1. Limpiar toda la sesión (borra UsuarioNombre, UsuarioRol, etc.)
            HttpContext.Session.Clear();

            // 2. Redirigir al Login
            return RedirectToAction("Login", "Acceso");
        }



    }
}
