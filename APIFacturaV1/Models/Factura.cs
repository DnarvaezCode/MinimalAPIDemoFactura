using System.ComponentModel.DataAnnotations;

namespace APIFacturaV1.Models
{
    public class Factura
    {
        public Factura()
        {
            DetalleFactura = new HashSet<DetalleFactura>();
            Fecha = DateTime.Now;
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "El cliente es requerido.")]
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "El número de factura es requerido.")]
        public string NumeroFactura { get; set; }
        [Required(ErrorMessage = "La fecha de la factura es requerido.")]
        public DateTime Fecha { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual IEnumerable<DetalleFactura> DetalleFactura { get; set; }
    }
}
