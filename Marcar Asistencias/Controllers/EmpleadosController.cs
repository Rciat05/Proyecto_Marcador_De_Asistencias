using Marcar_Asistencias.Repositories;
using Microsoft.AspNetCore.Mvc;
using proyectometodologias.models;

namespace Marcar_Asistencias.Controllers
{
    public class EmpleadosController : Controller
    {

        private readonly IEmpleadosRepository _empleadosRepository;

        public EmpleadosController(IEmpleadosRepository empleadosRepository)
        {
            _empleadosRepository = empleadosRepository;
        }

        public IActionResult Index()
        {
            return View(_empleadosRepository.GetAll());
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmpleadosModel empleados)
        {
            try
            {
                _empleadosRepository.Add(empleados);

                TempData["message"] = "Datos guardados con exito";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return View(empleados);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var empleados = _empleadosRepository.GetById(id);

            if (empleados == null)
            {
                return NotFound();
            }

            return View(empleados);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmpleadosModel empleados)
        {
            try
            {
                _empleadosRepository.Edit(empleados);

                TempData["editEmpleado"] = "Se editó el empleado";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(empleados);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _empleadosRepository.Delete(id);

                TempData["deleteEmpleados"] = "Se eliminó el empleado con éxito";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["deleteClient"] = ex.Message;

                return RedirectToAction(nameof(Index)); // Redirige a Index en caso de error
            }
        }

    }
}

