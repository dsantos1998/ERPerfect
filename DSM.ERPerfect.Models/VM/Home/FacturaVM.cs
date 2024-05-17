using DSM.ERPerfect.Models.Cookies;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Queries;

namespace DSM.ERPerfect.Models.VM.Home
{
    public class FacturaVM
    {
        public CookieUsuario? CookieUsuario { get; set; }
        public List<Factura> Facturas { get; set; }
        public List<FacturaServicio> FacturasServicios { get; set; }
        public List<Cliente> Clientes { get; set; }
        public List<Servicio> Servicios { get; set; }
        public List<Tarifa> Tarifas { get; set; }
        public List<TarifaServicioQuery> TarifasServicios { get; set; }
        public List<FormaPago> FormasPago { get; set; }
        public Factura? Factura { get; set; }
        public FacturaServicio? FacturaServicio { get; set; }
    }
}
