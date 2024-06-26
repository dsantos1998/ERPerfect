﻿using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;
using DSM.ERPerfect.Models.Queries;

namespace DSM.ERPerfect.BLL
{
    public class TarifaServicioBusiness : ITarifaServicioBusiness
    {
        private ITarifaServicioRepository _tarifaServicioRepository { get; init; }

        public TarifaServicioBusiness(ITarifaServicioRepository tarifaServicioRepository)
        {
            _tarifaServicioRepository = tarifaServicioRepository;
        }

        #region Métodos públicos

        public ResultInfo<List<TarifaServicioQuery>> GetTarifaServicio()
        {
            return _tarifaServicioRepository.GetTarifaServicio();
        }

        public ResultInfo<TarifaServicioQuery> GetTarifaServicioById(int id)
        {
            return _tarifaServicioRepository.GetTarifaServicioById(id);
        }

        public ResultInfo<List<TarifaServicioQuery>> GetTarifaServicioByIdTarifa(int id)
        {
            return _tarifaServicioRepository.GetTarifaServicioByIdTarifa(id);
        }

        public ResultInfo<List<TarifaServicioQuery>> GetTarifaServicioByIdServicio(int id)
        {
            return _tarifaServicioRepository.GetTarifaServicioByIdServicio(id);
        }

        public ResultInfo<int> NewTarifaServicio(TarifaServicio item)
        {
            return _tarifaServicioRepository.NewTarifaServicio(item);
        }

        public ResultInfo<int> UpdateTarifaServicio(TarifaServicio item)
        {
            return _tarifaServicioRepository.UpdateTarifaServicio(item);
        }

        public ResultInfo<int> DisabledTarifaServicio(int id)
        {
            return _tarifaServicioRepository.DisabledTarifaServicio(id);
        }

        public ResultInfo<int> EnabledTarifaServicio(int id)
        {
            return _tarifaServicioRepository.EnabledTarifaServicio(id);
        }

        public ResultInfo<int> DeleteTarifaServicio(int id)
        {
            return _tarifaServicioRepository.DeleteTarifaServicio(id);
        }


        #endregion

        #region Métodos privados



        #endregion

    }
}
