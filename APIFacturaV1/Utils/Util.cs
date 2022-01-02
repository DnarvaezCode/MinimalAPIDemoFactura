using Microsoft.Data.SqlClient;
using System.Data;

namespace APIFacturaV1.Utils
{
    public static class Util
    {
        public const string spObtenerCliente = "spObtenerCliente";
        public const string spObtenerClientes = "spObtenerClientes";
        public const string spInsertarCliente = "spInsertarCliente";
        public const string spActualizarCliente = "spActualizarCliente";
        public const string spEliminarCliente = "spEliminarCliente";

        public static SqlParameter ParameterId()
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
    }
}
