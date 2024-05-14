using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Helpers;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;
using DSM.ERPerfect.Models.VM.Login;

namespace DSM.ERPerfect.BLL
{
    public class UsuarioBusiness : IUsuarioBusiness
    {
        private IUsuarioRepository _usuarioRepository { get; init; }

        public UsuarioBusiness(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        #region Métodos públicos

        public ResultInfo<List<Usuario>> GetUsuarios()
        {
            return _usuarioRepository.GetUsuarios();
        }

        public ResultInfo<Usuario> GetUsuarioByRowGUID(Guid guid)
        {
            return _usuarioRepository.GetUsuarioByRowGUID(guid);
        }

        public ResultInfo<Usuario> GetUsuarioByLogin(string login)
        {
            return _usuarioRepository.GetUsuarioByLogin(login);
        }

        public ResultInfo<int> NewUsuario(Usuario item)
        {
            // Hash password
            byte[] derivedKey = EncryptHelper.GenerateDerivedKey(item.Password);
            string passwordHashed = EncryptHelper.EncryptPassword(item.Password, derivedKey);
            item.Password = passwordHashed;

            return _usuarioRepository.NewUsuario(item);
        }

        public ResultInfo<int> UpdateUsuario(Usuario item, bool resetPassword = false)
        {
            if (resetPassword)
            {
                // Hash password
                byte[] derivedKey = EncryptHelper.GenerateDerivedKey(item.Password);
                string passwordHashed = EncryptHelper.EncryptPassword(item.Password, derivedKey);
                item.Password = passwordHashed;
            }

            return _usuarioRepository.UpdateUsuario(item);
        }

        public ResultInfo<int> DisabledUsuario(int idUsuario)
        {
            return _usuarioRepository.DisabledUsuario(idUsuario);
        }

        public ResultInfo<int> EnabledUsuario(int idUsuario)
        {
            return _usuarioRepository.EnabledUsuario(idUsuario);
        }

        public ResultInfo<int> DeleteUsuario(int idUsuario)
        {
            return _usuarioRepository.DeleteUsuario(idUsuario);
        }

        public ResultInfo<Usuario> GetUsuarioById(int idUsuario)
        {
            return _usuarioRepository.GetUsuarioById(idUsuario);
        }

        #endregion

        #region Métodos privados



        #endregion

    }
}
