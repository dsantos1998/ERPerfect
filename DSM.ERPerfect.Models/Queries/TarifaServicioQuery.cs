using DSM.ERPerfect.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSM.ERPerfect.Models.Queries
{
    public class TarifaServicioQuery
    {
        public int IdTarifaServicio { get; set; }
        public int IdTarifa { get; set; }
        public int IdServicio { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string TarifaDescripcion { get; set; }
        public string ServicioDescripcion { get; set; }
    }
}
