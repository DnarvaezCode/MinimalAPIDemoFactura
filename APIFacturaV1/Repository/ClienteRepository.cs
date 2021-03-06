using APIFacturaV1.Context;
using APIFacturaV1.Models;
using APIFacturaV1.Repository.Interfaces;
using APIFacturaV1.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace APIFacturaV1.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly APIFacturaContext _context;

        public ClienteRepository(APIFacturaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método para eliminar cliente por id.
        /// </summary>
        /// <param name="id">Parámetro a filtrar.</param>
        /// <returns>Retorna un entero con el número de fila afectada.</returns>
        public async Task<int> EliminarClienteAsync(int id)
        {
            try
            {
                return await _context.Database.ExecuteSqlRawAsync($"{Util.spEliminarCliente}  @Id", new SqlParameter("@Id", id));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para insertar cliente.
        /// </summary>
        /// <param name="model">Data del cliente</param>
        /// <returns>Retorna un entero con el id del nuevo registro.</returns>
        public async Task<int> InsertarClienteAsync(Cliente model)
        {
            try
            {
                //var nombreParam = new SqlParameter("Nombres", model.Nombres);
                //await _context.Database.ExecuteSqlRawAsync("EXEC spInsertarCliente @Id output, @Nombres", new[] { idParam, nombreParam });
                var idParameter = ParameterId();
                await _context.Database.ExecuteSqlRawAsync($"{Util.spInsertarCliente} @Id output,@Nombres,@Apellidos,@Direccion,@Telefono,@Correo, @FechaNacimiento",
                idParameter,
                 new SqlParameter("@Nombres", model.Nombres),
                 new SqlParameter("@Apellidos", model.Apellidos),
                 new SqlParameter("@Direccion", model.Direccion),
                 new SqlParameter("@Telefono", model.Telefono),
                 new SqlParameter("@Correo", model.Correo),
                 new SqlParameter("@FechaNacimiento", model.FechaNacimiento));
                return Convert.ToInt32(idParameter.Value);
            }
            catch (Exception)
            {
                return 0;
            }

        }
        /// <summary>
        /// Método para agregar y obtener el parámetro de salida.
        /// </summary>
        /// <returns>Objeto de tipo SqlParameter para obtener el valor de salida.</returns>
        private SqlParameter ParameterId()
        {
            SqlParameter parameter = new SqlParameter
            {
                ParameterName = "@Id",
                SqlDbType = SqlDbType.Int,
                //Se emite la dirección a este parametro.
                Direction = ParameterDirection.Output
            };
            return parameter;
        }

        /// <summary>
        /// Método para modificar cliente.
        /// </summary>
        /// <param name="model">Data del cliente.</param>
        /// <returns>Retorna un entero con el número de fila afectada.</returns>
        public async Task<int> ModificarClienteAsync(Cliente model)
        {
            try
            {
                return await _context.Database.ExecuteSqlRawAsync($"{Util.spActualizarCliente} @Id,@Nombres,@Apellidos,@Direccion,@Telefono,@Correo, @FechaNacimiento",
                  new SqlParameter("@Id", model.Id),
                  new SqlParameter("@Nombres", model.Nombres),
                  new SqlParameter("@Apellidos", model.Apellidos),
                  new SqlParameter("@Direccion", model.Direccion),
                  new SqlParameter("@Telefono", model.Telefono),
                  new SqlParameter("@Correo", model.Correo),
                  new SqlParameter("@FechaNacimiento", model.FechaNacimiento));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para obtener cliente por id.
        /// </summary>
        /// <param name="id">Parámetro a filtrar.</param>
        /// <returns>Retorna un objeto de tipo Cliente.</returns>
        public async Task<Cliente> ObtenerClientePorIdAsync(int id)
        {
            //return await _context.Cliente.FromSqlRaw($"SELECT * From Cliente Where Id = @Id", new SqlParameter("@Id", id)).FirstOrDefaultAsync();
            var resultado = await _context.Cliente.FromSqlRaw<Cliente>("spObtenerCliente  @Id", new SqlParameter("@Id", id)).AsNoTracking().ToListAsync();
            return resultado.FirstOrDefault();
        }

        /// <summary>
        /// Método para obtener todos los clientes.
        /// </summary>
        /// <returns>Retorna una lista de tipo Cliente.</returns>
        public async Task<IEnumerable<Cliente>> ObtenerClientesAsync()
        {
            return await _context.Cliente.FromSqlRaw<Cliente>($"exec {Util.spObtenerClientes}").ToListAsync();
        }
    }
}
