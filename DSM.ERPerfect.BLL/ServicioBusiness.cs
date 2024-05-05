using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.DAL.Interfaces;

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



        #endregion

        #region Métodos privados



        #endregion

    }
}
