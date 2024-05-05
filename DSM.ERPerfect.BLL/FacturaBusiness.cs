using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.DAL.Interfaces;

namespace DSM.ERPerfect.BLL
{
    public class FacturaBusiness : IFacturaBusiness
    {
        private IFacturaRepository _facturaRepository { get; init; }

        public FacturaBusiness(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        #region Métodos públicos



        #endregion

        #region Métodos privados



        #endregion

    }
}
