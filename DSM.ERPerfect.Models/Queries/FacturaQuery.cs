using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSM.ERPerfect.Models.Queries
{
    public class FacturaQuery
    {
        public int IdFactura { get; set; }
        public int IdCliente { get; set; }
        public int IdFormaPago { get; set; }
        public string? NFactura { get; set; }
        public string? NAbono { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaCobro { get; set; }
        public DateTime? FechaAbono { get; set; }
    }
}
