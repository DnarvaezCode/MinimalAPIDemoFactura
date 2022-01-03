using APIFacturaV1.Context;
using APIFacturaV1.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APIFacturaV1.Repository
{
    public class StoreProcedureRepository<T> : IStoreProcedureRepository<T> where T : class
    {
        private readonly APIFacturaContext _context;

        public StoreProcedureRepository(APIFacturaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método genérico para listar o filtrar data con ó sin parámetro.
        /// </summary>
        /// <param name="storeProcedureName">Nombre del procedimiento almacenado.</param>
        /// <param name="parameters">Parámetro para realizar filtro.</param>
        /// <returns>Retorna una lista de tipo T.</returns>
        public async Task<IEnumerable<T>> GetDataAsync(string storeProcedureName, params object[] parameters)
        {
            return await _context.Set<T>().FromSqlRaw<T>(storeProcedureName, parameters).ToListAsync();
        }

        /// <summary>
        /// Método genérico para guardar, actualizar y eliminar data.
        /// </summary>
        /// <param name="storeProcedureName">Nombre del procedimiento almacenado.</param>
        /// <param name="sqlParameter">Parámetro de salida.</param>
        /// <param name="parameters">Parámetros de entrada.</param>
        /// <returns>Retorna el número de fila afectado o bien el parámetro de salida.</returns>
        public async Task<int> SaveDataAsync(string storeProcedureName, SqlParameter sqlParameter, params object[] parameters)
        {
            try
            {
                var result = await _context.Database.ExecuteSqlRawAsync(storeProcedureName, parameters);
                if (sqlParameter is not null) return Convert.ToInt32(sqlParameter.Value);
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
