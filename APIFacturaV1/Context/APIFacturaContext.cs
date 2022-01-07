using APIFacturaV1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIFacturaV1.Context
{
    public class APIFacturaContext : IdentityDbContext
    {
        public APIFacturaContext(DbContextOptions<APIFacturaContext> options) : base(options)
        {

        }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Factura> Factura { get; set; }
        public DbSet<DetalleFactura> DetalleFactura { get; set; }
    }
}
