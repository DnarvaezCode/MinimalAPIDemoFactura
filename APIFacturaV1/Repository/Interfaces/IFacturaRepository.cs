using APIFacturaV1.Models;

namespace APIFacturaV1.Repository.Interfaces
{
    public interface IFacturaRepository
    {
        Task<List<Factura>> GetAllOrderAsync();
        Task<Factura> GetOrderByIdAsync(int id);
        Task<int> AddOrderAsync(Factura factura);
    }
}
