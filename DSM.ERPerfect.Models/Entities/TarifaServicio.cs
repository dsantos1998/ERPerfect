﻿namespace DSM.ERPerfect.Models.Entities
{
    public class TarifaServicio
    {
        public int IdTarifaServicio { get; set; }
        public int IdTarifa { get; set; }
        public int IdServicio { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
