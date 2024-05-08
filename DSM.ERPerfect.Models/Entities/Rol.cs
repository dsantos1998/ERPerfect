namespace DSM.ERPerfect.Models.Entities
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
    }

    public enum Roles
    {
        Administrador,
        Usuario
    }
}
