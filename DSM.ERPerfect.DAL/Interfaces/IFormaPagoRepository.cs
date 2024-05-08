using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

namespace DSM.ERPerfect.DAL.Interfaces
{
    public interface IFormaPagoRepository
    {
        public ResultInfo<List<FormaPago>> GetFormasPago();
        public ResultInfo<FormaPago> GetFormaPagoById(int id);
        public ResultInfo<int> NewFormaPago(FormaPago item);
        public ResultInfo<int> UpdateFormaPago(FormaPago item);
        public ResultInfo<int> DisabledFormaPago(int id);
        public ResultInfo<int> EnabledFormaPago(int id);
        public ResultInfo<int> DeleteFormaPago(int id);
    }
}