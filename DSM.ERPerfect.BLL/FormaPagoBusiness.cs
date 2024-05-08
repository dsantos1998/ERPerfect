using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

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

        public ResultInfo<List<FormaPago>> GetFormasPago()
        {
            return _formaPagoRepository.GetFormasPago();
        }

        public ResultInfo<FormaPago> GetFormaPagoById(int id)
        {
            return _formaPagoRepository.GetFormaPagoById(id);
        }

        public ResultInfo<int> NewFormaPago(FormaPago item)
        {
            return _formaPagoRepository.NewFormaPago(item);
        }

        public ResultInfo<int> UpdateFormaPago(FormaPago item)
        {
            return _formaPagoRepository.UpdateFormaPago(item);
        }

        public ResultInfo<int> DisabledFormaPago(int id)
        {
            return _formaPagoRepository.DisabledFormaPago(id);
        }

        public ResultInfo<int> EnabledFormaPago(int id)
        {
            return _formaPagoRepository.EnabledFormaPago(id);
        }

        public ResultInfo<int> DeleteFormaPago(int id)
        {
            return _formaPagoRepository.DeleteFormaPago(id);
        }


        #endregion

        #region Métodos privados



        #endregion

    }
}
