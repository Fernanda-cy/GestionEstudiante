using Microsoft.AspNetCore.Mvc;
using GestionEstudiante.Models;
using GestionEstudiante.Repository;
using NuGet.Protocol.Core.Types;

namespace GestionEstudiante.Controllers
{
    public class EstudianteController : Controller
    {
        private readonly EstudianteRepository repo;

        public EstudianteController(EstudianteRepository repo)
        {
            this.repo = repo;
        }

       
        //listado
        public IActionResult Listado(string? Busqueda)
        {
            ViewData["FiltroActual"]= Busqueda;
            var lista= repo.listar(Busqueda);
            return View(lista);
        }

        //Crear
        public IActionResult Crear()
        {
            // 🛡️ SEGURIDAD DE FONDO
            if (HttpContext.Session.GetString("UsuarioRol") != "Admin")
            {
                return RedirectToAction("Listado"); // Si no es Admin, lo sacamos de aquí
            }
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Estudiante e)
        {
            repo.Insertar(e);
            return RedirectToAction("Listado");
        }

        // EDITAR (GET)
        public IActionResult Edit(int IdEstudiante)
        {
            // Si el ID es 0, significa que entraron sin seleccionar a nadie
            if (IdEstudiante == 0)
            {
                return RedirectToAction("Listado"); // Mándalos de vuelta a la lista
            }

            var estudiante = repo.Buscar(IdEstudiante);

            if (estudiante == null)
            {
                return NotFound(); // O RedirectToAction("Listado")
            }

            return View(estudiante);
        }

        // EDITAR (POST)
        [HttpPost]
        public IActionResult Edit(Estudiante e)
        {
            repo.Actualizar(e);
            return RedirectToAction("Listado");
        }

       

        //Eliminar
        public IActionResult Delete(int IdEstudiante)
        {
            var estudiante = repo.Buscar(IdEstudiante);
            if (estudiante == null)
                return NotFound();

            return View(estudiante);
        }


        // DELETE (POST) → ELIMINA
        [HttpPost]
        [HttpPost]
        public IActionResult DeleteConfirm(int IdEstudiante)
        {
            repo.Eliminar(IdEstudiante);
            return RedirectToAction("Listado");
        }
    }
}
