using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

namespace DSM.ERPerfect.BLL
{
    public class RolBusiness : IRolBusiness
    {
        private IRolRepository _rolRepository { get; init; }

        public RolBusiness(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        #region Métodos públicos

        public ResultInfo<List<Rol>> GetRoles()
        {
            return _rolRepository.GetRoles();
        }

        public ResultInfo<Rol> GetRolById(int id)
        {
            return _rolRepository.GetRolById(id);
        }

        public ResultInfo<Rol> GetRolByDescription(string description)
        {
            return _rolRepository.GetRolByDescription(description);
        }

        public ResultInfo<int> NewRol(Rol item)
        {
            return _rolRepository.NewRol(item);
        }

        public ResultInfo<int> UpdateRol(Rol item)
        {
            return _rolRepository.UpdateRol(item);
        }

        public ResultInfo<int> DisabledRol(int id)
        {
            return _rolRepository.DisabledRol(id);
        }

        public ResultInfo<int> EnabledRol(int id)
        {
            return _rolRepository.EnabledRol(id);
        }

        public ResultInfo<int> DeleteRol(int id)
        {
            return _rolRepository.DeleteRol(id);
        }


        #endregion

        #region Métodos privados



        #endregion

    }
}
