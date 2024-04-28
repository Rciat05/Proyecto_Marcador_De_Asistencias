using Dapper;
using Marcar_Asistencias.Data;
using Marcar_Asistencias.Models;
using proyectometodologias.models;
using System.Data;

namespace Marcar_Asistencias.Repositories
{
    public class VacacionesRepository : IVacacionesRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public VacacionesRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<VacacionesModel> GetAll()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spVacaciones_GetAll";

                return connection.Query<VacacionesModel>(
                                  storeProcedure,
                                  commandType: CommandType.StoredProcedure
                                    );
            }
        }

        public VacacionesModel? GetById(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spVacaciones_GetById";

                return connection.QueryFirstOrDefault<VacacionesModel>(
                                  storeProcedure,
                                  new { IDVacaciones = id },
                                  commandType: CommandType.StoredProcedure
                                    );
            }
        }

        public void Add(VacacionesModel vacaciones)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spVacaciones_Insert";

                connection.Execute
                    (
                        storeProcedure,
                        new { vacaciones.EmpleadoID, vacaciones.FechaInicio, vacaciones.FechaFin, vacaciones.Tipo },
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public void Delete(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {

                string storeProcedure = "dbo.spVacaciones_Delete";

                connection.Execute(
                     storeProcedure,
                     new { IDVacaciones = id },
                     commandType: CommandType.StoredProcedure
                 );
            }
        }

        public void Edit(VacacionesModel vacaciones)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spVacaciones_Update";

                connection.Execute(
                        storeProcedure,
                        vacaciones,
                        commandType: CommandType.StoredProcedure
                    );
            }
        }
    }
}
