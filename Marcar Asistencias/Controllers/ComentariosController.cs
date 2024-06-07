using Marcar_Asistencias.Models;
using Marcar_Asistencias.Repositories;
using Marcar_Asistencias.Services.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;

namespace Marcar_Asistencias.Controllers
{
    public class ComentariosController : Controller
    {

        private readonly IComentariosRepository _comentariosRepository;
		private readonly IEmailService _emailService;

		public ComentariosController(IComentariosRepository comentariosRepository, IEmailService emailService)
        {
            _comentariosRepository = comentariosRepository;
            _emailService = emailService;
        }

        public ActionResult Index()
        {
            return View(_comentariosRepository.GetAll());
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
        public ActionResult Create(ComentariosModel comentarios)
        {
            try
            {
                _comentariosRepository.Add(comentarios);

				TempData["message"] = "Datos guardados con exito";

				string email = "developers@help.sv.com";
				string subject = comentarios.Tipo;
				string type = "Comment";
				_emailService.SendEmail(email, comentarios.Texto, subject, type);

				return RedirectToAction(nameof(Index));
			}
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return View(comentarios);
            }
        }

        
        public ActionResult Edit(int id)
        {
            var comentarios = _comentariosRepository.GetById(id);

            if (comentarios == null)
            {
                return NotFound();
            }

			string email = "developers@help.sv.com";
			string subject = comentarios.Tipo;
			string type = "Comment";
			_emailService.SendEmail(email, comentarios.Texto, subject, type);

			return View(comentarios);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ComentariosModel comentarios)
        {
            try
            {
                _comentariosRepository.Edit(comentarios);

                TempData["editComentario"] = "Se editó el comentario.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(comentarios);
            }
        }

        
        public ActionResult Delete(int id)
        {

            var comentarios = _comentariosRepository.GetById(id);

            if (comentarios == null)
            {
                return NotFound();
            }

            return View(comentarios);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ComentariosModel comentarios)
        {
            try
            {
                _comentariosRepository.Delete(comentarios.ComentarioNotaID);

                TempData["deleteComentario"] = "Se eliminó el comentario con éxito";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["deleteComment"] = ex.Message;

                return View(comentarios);
            }
        }
    }
}
