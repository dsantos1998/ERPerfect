using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

namespace DSM.ERPerfect.BLL.Interfaces
{
    public interface IUsuarioBusiness
    {
        public ResultInfo<List<Usuario>> GetUsuarios();
        public ResultInfo<Usuario> GetUsuarioByRowGUID(Guid guid);
        public ResultInfo<Usuario> GetUsuarioByLogin(string login);
        public ResultInfo<int> NewUsuario(Usuario item);
        public ResultInfo<int> UpdateUsuario(Usuario item, bool resetPassword = false);
        public ResultInfo<int> DisabledUsuario(int idUsuario);
        public ResultInfo<int> EnabledUsuario(int idUsuario);
        public ResultInfo<int> DeleteUsuario(int idUsuario);
        public ResultInfo<Usuario> GetUsuarioById(int idUsuario);
    }
}