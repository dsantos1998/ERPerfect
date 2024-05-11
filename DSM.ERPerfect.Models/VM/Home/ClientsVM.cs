using DSM.ERPerfect.Models.Cookies;
using DSM.ERPerfect.Models.Entities;

namespace DSM.ERPerfect.Models.VM.Home
{
    public class ClientsVM
    {
        public CookieUsuario CookieUsuario { get; set; }
        public List<Cliente> Clientes { get; set; }
    }
}
