namespace APIFacturaV1.Models
{
    public class Factura
    {
        public Factura()
        {
            DetalleFactura = new HashSet<DetalleFactura>();
        }
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual IEnumerable<DetalleFactura> DetalleFactura { get; set; }
    }
}
