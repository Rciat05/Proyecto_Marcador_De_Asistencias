using Marcar_Asistencias.Models;
using Marcar_Asistencias.Repositories;
using Microsoft.AspNetCore.Mvc;
using proyectometodologias.models;

namespace Marcar_Asistencias.Controllers
{
    public class AusenciasController : Controller
    {
        private readonly IAusenciasRepository _ausenciasRepository;

        public AusenciasController(IAusenciasRepository ausenciasRepository)
        {
            _ausenciasRepository = ausenciasRepository;
        }

        public IActionResult Index()
        {
            return View(_ausenciasRepository.GetAll());
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
        public IActionResult Create(AusenciasModels ausencia)
        {
            try
            {
                _ausenciasRepository.Add(ausencia);
                TempData["message"] = "Datos guardados con éxito";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return View(ausencia);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
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
        public IActionResult Edit(AusenciasModels ausencia)
        {
            try
            {
                _ausenciasRepository.Edit(ausencia);
                TempData["editAusencias"] = "Se editó con éxito";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(ausencia);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(AusenciasModels ausencia)
        {
            try
            {
                _ausenciasRepository.Delete(ausencia.IDAusencias);
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
