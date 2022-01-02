using System.ComponentModel.DataAnnotations;

namespace APIFacturaV1.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "La dirección es requerido.")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El telefono es requerido.")]
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Estado { get; set; }
    }
}
