namespace DSM.ERPerfect.Models.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        public Guid RowGUID { get; set; }
        public int Intentos { get; set; }
        public DateTime? FechaBloqueo { get; set; }
        public DateTime? FechaUltimoLogin { get; set; }
    }
}
