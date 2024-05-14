using DSM.ERPerfect.Models.Cookies;
using DSM.ERPerfect.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSM.ERPerfect.Models.VM.BackOffice
{
    public class UsuarioVM
    {
        public CookieUsuario CookieUsuario { get; set; }
        public List<Usuario> Usuarios { get; set; }
        public Usuario? Usuario { get; set; }
        public List<Rol> Roles { get; set; }
        public bool Active { get; set; }
    }
}
