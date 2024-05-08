using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

namespace DSM.ERPerfect.DAL.Interfaces
{
    public interface IRolRepository
    {
        public ResultInfo<List<Rol>> GetRoles();
        public ResultInfo<Rol> GetRolById(int id);
        public ResultInfo<Rol> GetRolByDescription(string description);
        public ResultInfo<int> NewRol(Rol item);
        public ResultInfo<int> UpdateRol(Rol item);
        public ResultInfo<int> DisabledRol(int id);
        public ResultInfo<int> EnabledRol(int id);
        public ResultInfo<int> DeleteRol(int id);
    }
}