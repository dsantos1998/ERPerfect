using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

namespace DSM.ERPerfect.BLL.Interfaces
{
    public interface ITarifaServicioBusiness
    {
        public ResultInfo<List<TarifaServicio>> GetTarifaServicio();
        public ResultInfo<TarifaServicio> GetTarifaServicioById(int id);
        public ResultInfo<List<TarifaServicio>> GetTarifaServicioByIdTarifa(int id);
        public ResultInfo<List<TarifaServicio>> GetTarifaServicioByIdServicio(int id);
        public ResultInfo<int> NewTarifaServicio(TarifaServicio item);
        public ResultInfo<int> UpdateTarifaServicio(TarifaServicio item);
        public ResultInfo<int> DisabledTarifaServicio(int id);
        public ResultInfo<int> EnabledTarifaServicio(int id);
        public ResultInfo<int> DeleteTarifaServicio(int id);
    }
}