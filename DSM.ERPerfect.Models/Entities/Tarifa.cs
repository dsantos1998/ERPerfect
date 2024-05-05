namespace DSM.ERPerfect.Models.Entities
{
    public class Tarifa
    {
        public int IdTarifa { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
