using APIFacturaV1.Models;

namespace APIFacturaV1.Repository.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ObtenerClientesAsync();
        Task<Cliente> ObtenerClientePorIdAsync(int id);
        Task<int> InsertarClienteAsync(Cliente model);
        Task<int> ModificarClienteAsync(Cliente model);
        Task<int> EliminarClienteAsync(int id);
    }
}
