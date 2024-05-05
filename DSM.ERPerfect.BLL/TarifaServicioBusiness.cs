using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.DAL.Interfaces;

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



        #endregion

        #region Métodos privados



        #endregion

    }
}
