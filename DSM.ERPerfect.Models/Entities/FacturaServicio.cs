using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSM.ERPerfect.Models.Entities
{
    public class FacturaServicio
    {
        public int IdFacturaServicio { get; set; }
        public int IdFactura { get; set; }
        public int IdServicio { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public int? IdUsuarioEliminacion { get; set; }
    }
}
