using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.DAL.Interfaces;

namespace DSM.ERPerfect.BLL
{
    public class FacturaServicioBusiness : IFacturaServicioBusiness
    {
        private IFacturaServicioRepository _facturaServicioRepository { get; init; }

        public FacturaServicioBusiness(IFacturaServicioRepository facturaServicioRepository)
        {
            _facturaServicioRepository = facturaServicioRepository;
        }

        #region Métodos públicos



        #endregion

        #region Métodos privados



        #endregion

    }
}
