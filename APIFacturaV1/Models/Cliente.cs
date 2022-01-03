using System.ComponentModel.DataAnnotations;

namespace APIFacturaV1.Models
{
    public class Cliente
    {
        public Cliente()
        {
            Factura = new HashSet<Factura>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de la categoria es requerido.")]
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "La dirección es requerido.")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El telefono es requerido.")]
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Estado { get; set; }
        public virtual IEnumerable<Factura> Factura { get; set; }
    }
}
