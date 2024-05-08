using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

namespace DSM.ERPerfect.BLL
{
    public class ServicioBusiness : IServicioBusiness
    {
        private IServicioRepository _servicioRepository { get; init; }

        public ServicioBusiness(IServicioRepository servicioRepository)
        {
            _servicioRepository = servicioRepository;
        }

        #region Métodos públicos

        public ResultInfo<List<Servicio>> GetServicios()
        {
            return _servicioRepository.GetServicios();
        }

        public ResultInfo<Servicio> GetServicioById(int id)
        {
            return _servicioRepository.GetServicioById(id);
        }

        public ResultInfo<int> NewServicio(Servicio item)
        {
            return _servicioRepository.NewServicio(item);
        }

        public ResultInfo<int> UpdateServicio(Servicio item)
        {
            return _servicioRepository.UpdateServicio(item);
        }

        public ResultInfo<int> DisabledServicio(int id)
        {
            return _servicioRepository.DisabledServicio(id);
        }

        public ResultInfo<int> EnabledServicio(int id)
        {
            return _servicioRepository.EnabledServicio(id);
        }

        public ResultInfo<int> DeleteServicio(int id)
        {
            return _servicioRepository.DeleteServicio(id);
        }


        #endregion

        #region Métodos privados



        #endregion

    }
}
