using DSM.ERPerfect.Models.Cookies;
using DSM.ERPerfect.Models.Entities;

namespace DSM.ERPerfect.Models.VM.Home
{
    public class ClienteVM
    {
        public CookieUsuario CookieUsuario { get; set; }
        public List<Cliente> Clientes { get; set; }
        public Cliente? Cliente { get; set; }
        public bool Active { get; set; }
    }
}
