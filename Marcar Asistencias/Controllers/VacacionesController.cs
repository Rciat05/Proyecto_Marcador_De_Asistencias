using Marcar_Asistencias.Models;
using Marcar_Asistencias.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marcar_Asistencias.Controllers
{
    public class VacacionesController : Controller
    {

        private readonly IVacacionesRepository _vacacionesRepository;

        public VacacionesController(IVacacionesRepository vacacionesRepository)
        {
            _vacacionesRepository = vacacionesRepository;
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
                _vacacionesRepository.Add(vacaciones);

                TempData["message"] = "Datos guardados con exito";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return View(vacaciones);
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
