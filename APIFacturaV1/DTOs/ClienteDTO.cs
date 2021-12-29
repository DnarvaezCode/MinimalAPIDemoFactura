using System.ComponentModel.DataAnnotations;

namespace APIFacturaV1.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo requerido.")]
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Estado { get; set; }
    }
}
