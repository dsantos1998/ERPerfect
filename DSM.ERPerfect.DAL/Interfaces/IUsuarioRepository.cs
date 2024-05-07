using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

namespace DSM.ERPerfect.DAL.Interfaces
{
    public interface IUsuarioRepository
    {
        public ResultInfo<List<Usuario>> GetUsuarios();
        public ResultInfo<Usuario> GetUsuarioByRowGUID(Guid guid);
        public ResultInfo<Usuario> GetUsuarioByLogin(string login);
        public ResultInfo<int> NewUsuario(Usuario item);
        public ResultInfo<int> UpdateUsuario(Usuario item);
        public ResultInfo<int> DisabledUsuario(int idUsuario);
        public ResultInfo<int> EnabledUsuario(int idUsuario);
        public ResultInfo<int> DeleteUsuario(int idUsuario);
    }
}