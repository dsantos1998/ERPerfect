namespace DSM.ERPerfect.Models.Entities
{
    public class Parametro
    {
        public int IdParametro { get; set; }
        public string Codigo { get; set; }
        public string Valor { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
    }

    public enum ParametroCodigo
    {
        NFACTURA,
        NABONO
    }
}
