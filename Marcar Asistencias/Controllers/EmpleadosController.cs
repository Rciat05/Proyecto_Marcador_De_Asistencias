using Marcar_Asistencias.Repositories;
using Marcar_Asistencias.Services.Email;
using Microsoft.AspNetCore.Mvc;
using proyectometodologias.models;

namespace Marcar_Asistencias.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly IEmpleadosRepository _empleadosRepository;
        private readonly IEmailService _emailService;

        public EmpleadosController(IEmpleadosRepository empleadosRepository, IEmailService emailService)
        {
            _empleadosRepository = empleadosRepository;
            _emailService = emailService;
        }

        public ActionResult Index()
        {
            return View(_empleadosRepository.GetAll());
        }

        public ActionResult Details()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmpleadosModel empleados)
        {
            try
            {
                _empleadosRepository.Add(empleados);
                TempData["message"] = "Datos guardados con éxito";

                string email = "HeladosSarita@gmail.com";
                string subject = "Nuevo empleado Sarita";
                string body = "Bienvenido a tu nuevo empleo de Helados Sarita! un gusto " + empleados.Nombre;

                _emailService.SendEmail(email, empleados.Nombre, subject, body);

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

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var empleados = _empleadosRepository.GetById(id);

            if (empleados == null)
            {
                return NotFound();
            }

            return View(empleados);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _empleadosRepository.Delete(id);
                TempData["deleteEmpleados"] = "Se eliminó el empleado con éxito";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["deleteEmployee"] = ex.Message;

                var empleados = _empleadosRepository.GetById(id);
                if (empleados == null)
                {
                    return NotFound();
                }
                return View(empleados);
            }
        }
    }
}
