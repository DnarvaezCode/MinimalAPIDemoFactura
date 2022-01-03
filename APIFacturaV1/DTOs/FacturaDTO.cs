using System.ComponentModel.DataAnnotations;

namespace APIFacturaV1.DTOs
{
    public class FacturaDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El cliente es requerido.")]
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "El número de factura es requerido.")]
        public string NumeroFactura { get; set; }
        public ClienteDTO Cliente { get; set; }
        public IEnumerable<DetalleFacturaDTO> DetalleFactura { get; set; }
    }
}
