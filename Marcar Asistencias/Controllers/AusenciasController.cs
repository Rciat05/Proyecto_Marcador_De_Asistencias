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

        public ActionResult Index()
        {
            return View(_ausenciasRepository.GetAll());
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
        public ActionResult Create(AusenciasModels ausencia)
        {
            try
            {
                _ausenciasRepository.Add(ausencia);
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
        public ActionResult Edit(AusenciasModels ausencia)
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
