namespace APIFacturaV1.Models
{
    public class Cliente
    {
        public Cliente()
        {
            Factura = new HashSet<Factura>();
        }
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Estado { get; set; }
        public virtual IEnumerable<Factura> Factura { get; set; }
    }
}
