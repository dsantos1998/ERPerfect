using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

namespace DSM.ERPerfect.BLL
{
    public class TarifaBusiness : ITarifaBusiness
    {
        private ITarifaRepository _tarifaRepository { get; init; }

        public TarifaBusiness(ITarifaRepository tarifaRepository)
        {
            _tarifaRepository = tarifaRepository;
        }

        #region Métodos públicos

        public ResultInfo<List<Tarifa>> GetTarifas()
        {
            return _tarifaRepository.GetTarifas();
        }

        public ResultInfo<Tarifa> GetTarifaById(int id)
        {
            return _tarifaRepository.GetTarifaById(id);
        }

        public ResultInfo<int> NewTarifa(Tarifa item)
        {
            return _tarifaRepository.NewTarifa(item);
        }

        public ResultInfo<int> UpdateTarifa(Tarifa item)
        {
            return _tarifaRepository.UpdateTarifa(item);
        }

        public ResultInfo<int> DisabledTarifa(int id)
        {
            return _tarifaRepository.DisabledTarifa(id);
        }

        public ResultInfo<int> EnabledTarifa(int id)
        {
            return _tarifaRepository.EnabledTarifa(id);
        }

        public ResultInfo<int> DeleteTarifa(int id)
        {
            return _tarifaRepository.DeleteTarifa(id);
        }


        #endregion

        #region Métodos privados



        #endregion

    }
}
