using Marcar_Asistencias.Models;
using Marcar_Asistencias.Repositories;
using Marcar_Asistencias.Services.Email;
using Microsoft.AspNetCore.Mvc;

namespace Marcar_Asistencias.Controllers
{
    public class HorariosController : Controller
    {
        private readonly IHorariosRepository _horariosRepository;
        private readonly IEmailService _emailService;

        public HorariosController(IHorariosRepository horariosRepository, IEmailService emailService)
        {
            _horariosRepository = horariosRepository;
            _emailService = emailService;
        }

        public ActionResult Index()
        {
            try
            {
                var horarios = _horariosRepository.GetAll();
                return View(horarios);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Error: {ex.Message}";
                return View("Error");
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                var horario = _horariosRepository.GetById(id);
                if (horario == null)
                {
                    return NotFound();
                }
                return View(horario);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Error: {ex.Message}";
                return View("Error");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HorariosModel horarios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _horariosRepository.Add(horarios);

                    TempData["message"] = "Datos guardados con éxito";

                    string email = "HeladosSarita@gmail.com";
                    string subject = "Nuevo horario registrado";
                    string type = "Horarios";
                    _emailService.SendEmail(email, $"Un nuevo horario de trabajo ha sido registrado los días {horarios.DiasLaborables}", subject, type);

                    return RedirectToAction(nameof(Index));
                }
                return View(horarios);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Error: {ex.Message}";
                return View(horarios);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var horarios = _horariosRepository.GetById(id);
                if (horarios == null)
                {
                    return NotFound();
                }
                return View(horarios);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Error: {ex.Message}";
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HorariosModel horarios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _horariosRepository.Edit(horarios);
                    TempData["editEmpleado"] = "Se editó el horario";
                    return RedirectToAction(nameof(Index));
                }
                return View(horarios);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Error: {ex.Message}";
                return View(horarios);
            }
        }

        [HttpGet]
        public ActionResult DeleteHorarios(int id)
        {
            try
            {
                var horarios = _horariosRepository.GetById(id);
                if (horarios == null)
                {
                    return NotFound();
                }
                return View(horarios);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Error: {ex.Message}";
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var horarios = _horariosRepository.GetById(id);
                if (horarios == null)
                {
                    return NotFound();
                }
                _horariosRepository.Delete(id);
                TempData["deleteHorarios"] = "Se eliminó el horario con éxito";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Error: {ex.Message}";
                return View("Error");
            }
        }
    }
}
