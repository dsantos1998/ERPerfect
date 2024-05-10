using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;
using DSM.ERPerfect.Models.Statistics;

namespace DSM.ERPerfect.BLL.Interfaces
{
    public interface IFacturaBusiness
    {
        public ResultInfo<List<Factura>> GetFacturas();
        public ResultInfo<List<Factura>> GetFacturasPending();
        public ResultInfo<Factura> GetFacturaById(int id);
        public ResultInfo<List<Factura>> GetFacturasByIdCliente(int idCliente);
        public ResultInfo<int> NewFactura(Factura item);
        public ResultInfo<int> UpdateFacturaPaid(Factura item);
        public ResultInfo<int> UpdateFacturaRefound(Factura item);
        public ResultInfo<int> DeleteFactura(int id);
        public ResultInfo<int> GetPendingBills();
        public ResultInfo<List<PaymentBills>> GetPaymentBills();
        public ResultInfo<List<Top5Servicios>> GetTop5ServicioMes();
    }
}