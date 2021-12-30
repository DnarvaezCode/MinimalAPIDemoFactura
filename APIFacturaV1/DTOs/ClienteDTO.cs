using System.ComponentModel.DataAnnotations;

namespace APIFacturaV1.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo requerido.")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Campo requerido.")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Campo requerido.")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Campo requerido.")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Campo requerido.")]
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Estado { get; set; }
    }
}
