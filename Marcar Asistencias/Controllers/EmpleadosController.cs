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

        public ActionResult Details(int id)
        {
            var empleado = _empleadosRepository.GetById(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmpleadosModel empleados)
        {
            if (!ModelState.IsValid)
            {
                return View(empleados);
            }

            try
            {
                _empleadosRepository.Add(empleados);
                TempData["message"] = "Datos guardados con éxito";

                string email = "HeladosSarita@gmail.com";
                string subject = "Nuevo empleado Sarita";
                string type = "Empleado";
                _emailService.SendEmail(email, $"El empleado {empleados.Nombre} ha sido registrado con éxito.", subject, type);
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
                TempData["editEmpleado"] = "Se editó el empleado con éxito";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["editEmpleado"] = ex.Message;
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
        public ActionResult DeleteEmplo(int id)
        {
            try
            {
                var existingEmpleado = _empleadosRepository.GetById(id);
                if (existingEmpleado == null)
                {
                    return NotFound();
                }

                _empleadosRepository.Delete(id);
                TempData["deleteEmpleado"] = "Se eliminó el empleado con éxito";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["deleteEmpleado"] = ex.Message;
                return RedirectToAction(nameof(Delete), new { id });
            }
        }
    }
}
