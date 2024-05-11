using Marcar_Asistencias.Models;

namespace Marcar_Asistencias.Repositories
{
    public interface IComentariosRepository
    {
        void Add(ComentariosModel comentarios);
        void Delete(int id);
        void Edit(ComentariosModel comentarios);
        IEnumerable<ComentariosModel> GetAll();
        ComentariosModel? GetById(int id);
    }
}