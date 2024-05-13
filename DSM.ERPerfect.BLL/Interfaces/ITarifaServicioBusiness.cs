using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;
using DSM.ERPerfect.Models.Queries;

namespace DSM.ERPerfect.BLL.Interfaces
{
    public interface ITarifaServicioBusiness
    {
        public ResultInfo<List<TarifaServicioQuery>> GetTarifaServicio();
        public ResultInfo<TarifaServicioQuery> GetTarifaServicioById(int id);
        public ResultInfo<List<TarifaServicioQuery>> GetTarifaServicioByIdTarifa(int id);
        public ResultInfo<List<TarifaServicioQuery>> GetTarifaServicioByIdServicio(int id);
        public ResultInfo<int> NewTarifaServicio(TarifaServicio item);
        public ResultInfo<int> UpdateTarifaServicio(TarifaServicio item);
        public ResultInfo<int> DisabledTarifaServicio(int id);
        public ResultInfo<int> EnabledTarifaServicio(int id);
        public ResultInfo<int> DeleteTarifaServicio(int id);
    }
}