using Marcar_Asistencias.Models;
using Marcar_Asistencias.Repositories;
using Marcar_Asistencias.Services.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marcar_Asistencias.Controllers
{
    public class VacacionesController : Controller
    {

        private readonly IVacacionesRepository _vacacionesRepository;
		private readonly IEmailService _emailService;

		public VacacionesController(IVacacionesRepository vacacionesRepository, IEmailService emailService)
        {
            _vacacionesRepository = vacacionesRepository;
			_emailService = emailService;
		}

        public ActionResult Index()
        {
            return View(_vacacionesRepository.GetAll());
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VacacionesModel vacaciones)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _vacacionesRepository.Add(vacaciones);

                    TempData["message"] = "Datos guardados con éxito";

                    string email = "empleado@empleado.com";
                    string subject = vacaciones.Tipo;
                    string type = "Vacation";
                    _emailService.SendEmail(email, $"¡Hola {vacaciones.EmpleadoID}! Sus vacaciones inician: {vacaciones.FechaInicio} y finalizan: {vacaciones.FechaFin}", subject, type);

                    return RedirectToAction(nameof(Index)); // Redirecciona al Index después de crear exitosamente
                }
                // Si el modelo no es válido, vuelve a mostrar la vista Create con los errores de validación
                return View(vacaciones);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Error: {ex.Message}";
                return View(vacaciones); // Si hay una excepción, vuelve a mostrar la vista Create con un mensaje de error
            }
        }


        // GET: VacacionesController/Edit/5
        public ActionResult Edit(int id)
        {
            var vacaciones = _vacacionesRepository.GetById(id);

            if (vacaciones == null)
            {
                return NotFound();
            }

			string email = "empleado@empleado.com";
			string subject = vacaciones.Tipo;
			string type = "Vacation";
			_emailService.SendEmail(email, $"¡Hola {vacaciones.EmpleadoID}! Sus vacaciones inician: {vacaciones.FechaInicio} y finalizan: {vacaciones.FechaFin}", subject, type);

			return View(vacaciones);
        }

        // POST: VacacionesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VacacionesModel vacaciones)
        {
            try
            {
                _vacacionesRepository.Edit(vacaciones);

                TempData["editEmpleado"] = "Se editó el ";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(vacaciones);
            }
        }

        // GET: VacacionesController/Delete/5
        public ActionResult Delete(int id)
        {
            var vacaciones = _vacacionesRepository.GetById(id);

            if (vacaciones == null)
            {
                return NotFound();
            }

            return View(vacaciones);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(VacacionesModel vacaciones)
        {
            try
            {
                _vacacionesRepository.Delete(vacaciones.IDVacaciones);

                TempData["deleteVacaciones"] = "Se eliminaron las vacaciones con éxito";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["deleteVacaciones"] = ex.Message;

                return View(vacaciones);
            }
        }
    }
}
