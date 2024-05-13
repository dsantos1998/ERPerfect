using DSM.ERPerfect.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSM.ERPerfect.Models.VM.BackOffice
{
    public class TarifaVM
    {
        public List<Tarifa>? Tarifas { get; set; }
        public bool Active { get; set; }
    }
}
