using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSM.ERPerfect.Models.Entities
{
    public class Factura
    {
        public int IdFactura { get; set; }
        public int IdCliente { get; set; }
        public int IdFormaPago { get; set; }
        public string? NFactura { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaCobro { get; set; }
    }
}
