using Marcar_Asistencias.Models;
using Marcar_Asistencias.Repositories;
using Marcar_Asistencias.Services.Email;
using Microsoft.AspNetCore.Mvc;
using proyectometodologias.models;

namespace Marcar_Asistencias.Controllers
{
    public class AusenciasController : Controller
    {
        private readonly IAusenciasRepository _ausenciasRepository;
        private readonly IEmailService _emailService;

        public AusenciasController(IAusenciasRepository ausenciasRepository, IEmailService emailService)
        {
            _ausenciasRepository = ausenciasRepository;
            _emailService = emailService;
        }

        public ActionResult Index()
        {
            var ausencias = _ausenciasRepository.GetAll();
            return View(ausencias);
        }

        public ActionResult Details(int id)
        {
            var ausencia = _ausenciasRepository.GetById(id);
            if (ausencia == null)
            {
                return NotFound();
            }
            return View(ausencia);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AusenciasModels ausencia)
        {
            try
            {
                _ausenciasRepository.Add(ausencia);
                TempData["message"] = "Datos guardados con éxito";

                string email = "HeladosSarita@gmail.com";
                string subject = "Nuevo asunto de ausencias";
                string type = "Ausencia";
                _emailService.SendEmail(email, $"Tu pedido de ausencia ha sido registrado con éxito por: {ausencia.TipoAusencia}", subject, type);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return View(ausencia);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var ausencia = _ausenciasRepository.GetById(id);
            if (ausencia == null)
            {
                return NotFound();
            }
            return View(ausencia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AusenciasModels ausencia)
        {
            if (id != ausencia.IDAusencias)
            {
                return BadRequest();
            }

            try
            {
                var existingAusencia = _ausenciasRepository.GetById(id);
                if (existingAusencia == null)
                {
                    return NotFound();
                }

                _ausenciasRepository.Edit(ausencia);
                TempData["editAusencias"] = "Se editó con éxito";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return View(ausencia);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var ausencia = _ausenciasRepository.GetById(id);
            if (ausencia == null)
            {
                return NotFound();
            }
            return View(ausencia);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, AusenciasModels ausencia)
        {
            try
            {
                var existingAusencia = _ausenciasRepository.GetById(id);
                if (existingAusencia == null)
                {
                    return NotFound();
                }

                _ausenciasRepository.Delete(id);
                TempData["deleteAusencias"] = "Se eliminó la ausencia con éxito";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["deleteAusencia"] = ex.Message;
                return View(ausencia);
            }
        }
    }
}
