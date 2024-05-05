namespace DSM.ERPerfect.Models.Entities
{
    public class Servicio
    {
        public int IdServicio { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
