using Dapper;
using Marcar_Asistencias.Data;
using Marcar_Asistencias.Models;
using proyectometodologias.models;
using System.Data;

namespace Marcar_Asistencias.Repositories
{
    public class ComentariosRepository : IComentariosRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public ComentariosRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<ComentariosModel> GetAll()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spComentarios_GetAll";

                return connection.Query<ComentariosModel>(
                                  storeProcedure,
                                  commandType: CommandType.StoredProcedure
                                    );
            }
        }

        public ComentariosModel? GetById(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spComentarios_GetById";

                return connection.QueryFirstOrDefault<ComentariosModel>(
                                  storeProcedure,
                                  new { ComentarioNotaID = id },
                                  commandType: CommandType.StoredProcedure
                                    );
            }
        }

        public void Add(ComentariosModel comentarios)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spComentarios_Insert";

                connection.Execute
                    (
                        storeProcedure,
                        new { comentarios.UsuarioID, comentarios.Tipo, comentarios.FechaHora, comentarios.Texto },
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public void Delete(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {

                string storeProcedure = "dbo.spComentarios_Delete";

                connection.Execute(
                     storeProcedure,
                     new { ComentarioNotaID = id },
                     commandType: CommandType.StoredProcedure
                 );
            }
        }

        public void Edit(ComentariosModel comentarios)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spComentarios_Update";

                connection.Execute(
                        storeProcedure,
                        comentarios,
                        commandType: CommandType.StoredProcedure
                    );
            }
        }
    }
}
