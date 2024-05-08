using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

namespace DSM.ERPerfect.DAL.Interfaces
{
    public interface IServicioRepository
    {
        public ResultInfo<List<Servicio>> GetServicios();
        public ResultInfo<Servicio> GetServicioById(int id);
        public ResultInfo<int> NewServicio(Servicio item);
        public ResultInfo<int> UpdateServicio(Servicio item);
        public ResultInfo<int> DisabledServicio(int id);
        public ResultInfo<int> EnabledServicio(int id);
        public ResultInfo<int> DeleteServicio(int id);
    }
}