using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.DAL.Interfaces;

namespace DSM.ERPerfect.BLL
{
    public class FormaPagoBusiness : IFormaPagoBusiness
    {
        private IFormaPagoRepository _formaPagoRepository { get; init; }

        public FormaPagoBusiness(IFormaPagoRepository formaPagoRepository)
        {
            _formaPagoRepository = formaPagoRepository;
        }

        #region Métodos públicos



        #endregion

        #region Métodos privados



        #endregion

    }
}
