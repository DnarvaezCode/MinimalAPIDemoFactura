using APIFacturaV1.Context;
using APIFacturaV1.Models;
using APIFacturaV1.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIFacturaV1.Repository
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly APIFacturaContext _context;

        public FacturaRepository(APIFacturaContext context)
        {
            _context = context;
        }
        public async Task<int> AddOrderAsync(Factura factura)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    await _context.Factura.AddAsync(factura);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return factura.Id;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return 0;
                }
            }
        }

        public async Task<List<Factura>> GetAllOrderAsync()
        {
            return await _context.Factura.Include(x => x.DetalleFactura).ToListAsync();
        }

        public async Task<Factura> GetOrderByIdAsync(int id)
        {
            return await _context.Factura.Include(x => x.DetalleFactura).FirstOrDefaultAsync();
        }
    }
}
