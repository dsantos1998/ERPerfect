using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSM.ERPerfect.Models.VM.BackOffice
{
    public class ServicioVM
    {
        public List<TarifaServicioQuery>? Servicios { get; set; }
        public List<Tarifa>? Tarifas { get; set; }
        public TarifaServicioQuery? TarifaServicio { get; set; }
        public bool Active { get; set; }
    }
}
