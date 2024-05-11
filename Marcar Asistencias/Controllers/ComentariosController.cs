using Marcar_Asistencias.Models;
using Marcar_Asistencias.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marcar_Asistencias.Controllers
{
    public class ComentariosController : Controller
    {

        private readonly IComentariosRepository _comentariosRepository;

        public ComentariosController(IComentariosRepository comentariosRepository)
        {
            _comentariosRepository = comentariosRepository;
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
