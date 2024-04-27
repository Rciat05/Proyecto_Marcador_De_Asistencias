using Marcar_Asistencias.Models;
using Marcar_Asistencias.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proyectometodologias.models;

namespace Marcar_Asistencias.Controllers
{
	public class HorariosController : Controller
	{

		private readonly IHorariosRepository _horariosRepository;

        public HorariosController(IHorariosRepository horariosRepository)
        {
            _horariosRepository = horariosRepository;
        }

        public ActionResult Index()
		{
			return View(_horariosRepository.GetAll());
		}

		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: HorariosController/Create
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
				_horariosRepository.Add(horarios);

				TempData["message"] = "Datos guardados con exito";
				return RedirectToAction(nameof(Index));

			}
			catch (Exception ex)
			{
				TempData["message"] = ex.Message;

				return View(horarios);
			}
		}

		// GET: HorariosController/Edit/5
		public ActionResult Edit(int id)
		{
			var horarios = _horariosRepository.GetById(id);

			if (horarios == null)
			{
				return NotFound();
			}

			return View(horarios);
		}

		// POST: HorariosController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(HorariosModel horarios)
		{
			try
			{
				_horariosRepository.Edit(horarios);

				TempData["editEmpleado"] = "Se editó el empleado";

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				return View(horarios);
			}
		}

		// GET: HorariosController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}


		[HttpGet]
		public ActionResult DeleteHorarios(int id)
		{
            var horarios = _horariosRepository.GetById(id);

            if (horarios == null)
            {
                return NotFound();
            }

            return View(horarios);
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(HorariosModel horarios)
		{
            try
            {
                _horariosRepository.Delete(horarios.HorarioID);

                TempData["deleteHorarios"] = "Se eliminó el horario con éxito";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["deleteClient"] = ex.Message;

                return View(horarios);
            }
        }
	}
}
