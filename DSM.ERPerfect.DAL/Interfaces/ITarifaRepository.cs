using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

namespace DSM.ERPerfect.DAL.Interfaces
{
    public interface ITarifaRepository
    {
        public ResultInfo<List<Tarifa>> GetTarifas();
        public ResultInfo<Tarifa> GetTarifaById(int id);
        public ResultInfo<int> NewTarifa(Tarifa item);
        public ResultInfo<int> UpdateTarifa(Tarifa item);
        public ResultInfo<int> DisabledTarifa(int id);
        public ResultInfo<int> EnabledTarifa(int id);
        public ResultInfo<int> DeleteTarifa(int id);
    }
}