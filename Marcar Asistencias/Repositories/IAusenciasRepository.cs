using Marcar_Asistencias.Models;
using proyectometodologias.models;

namespace Marcar_Asistencias.Repositories
{
    public interface IAusenciasRepository
    {
        void Add(AusenciasModels ausencias);
        void Delete(int id);
        void Edit(AusenciasModels ausencias);
        IEnumerable<AusenciasModels> GetAll();
        AusenciasModels? GetById(int id);
    }
}
