using Dapper;
using Marcar_Asistencias.Data;
using Marcar_Asistencias.Models;
using proyectometodologias.models;
using System.Data;

namespace Marcar_Asistencias.Repositories
{
    public class HorariosRepository : IHorariosRepository
    {

        private readonly ISqlDataAccess _dataAccess;

        public HorariosRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<HorariosModel> GetAll()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spHorarios_GetAll";

                return connection.Query<HorariosModel>(
                                  storeProcedure,
                                  commandType: CommandType.StoredProcedure
                                    );
            }
        }

        public HorariosModel? GetById(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.Horarios_GetById";

                return connection.QueryFirstOrDefault<HorariosModel>(
                                  storeProcedure,
                                  new { HorarioID = id },
                                  commandType: CommandType.StoredProcedure
                                    );
            }
        }

        public void Add(HorariosModel horarios)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spHorarios_Insert";

                connection.Execute
                    (
                        storeProcedure,
                        new { horarios.NombreHorario, horarios.DiasLaborables },
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public void Delete(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {

                string storeProcedure = "spHorarios_Delete";

                connection.Execute(
                     storeProcedure,
                     new { HorarioID = id },
                     commandType: CommandType.StoredProcedure
                 );
            }
        }

        public void Edit(HorariosModel horarios)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "spHorarios_Update";

                connection.Execute(
                        storeProcedure,
                        horarios,
                        commandType: CommandType.StoredProcedure
                    );
            }
        }
    }
}

