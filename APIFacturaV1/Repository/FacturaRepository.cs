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

        /// <summary>
        /// Método para agregar data de factura y su detalle.
        /// </summary>
        /// <param name="factura">Objeto que contiene la data de la factura.</param>
        /// <returns>Retorna el número de fila afectada.</returns>
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

        /// <summary>
        /// Método para obtener todas las factura con su detalle.
        /// </summary>
        /// <returns>Retorna una lista de tipo Factura.</returns>
        public async Task<List<Factura>> GetAllOrderAsync()
        {
            return await _context.Factura.Include(x => x.DetalleFactura).ToListAsync();
        }

        /// <summary>
        /// Método para obtener factura y su detalle por id.
        /// </summary>
        /// <param name="id">Parámetro a fitrar.</param>
        /// <returns>Retorna un objeto de tipo Factura.</returns>
        public async Task<Factura> GetOrderByIdAsync(int id)
        {
            return await _context.Factura.Include(x => x.DetalleFactura).FirstOrDefaultAsync();
        }
    }
}
