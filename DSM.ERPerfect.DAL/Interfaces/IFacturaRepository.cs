using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

namespace DSM.ERPerfect.DAL.Interfaces
{
    public interface IFacturaRepository
    {
        public ResultInfo<List<Factura>> GetFacturas();
        public ResultInfo<List<Factura>> GetFacturasPending();
        public ResultInfo<Factura> GetFacturaById(int id);
        public ResultInfo<List<Factura>> GetFacturasByIdCliente(int idCliente);
        public ResultInfo<int> NewFactura(Factura item);
        public ResultInfo<int> UpdateFacturaPaid(Factura item);
        public ResultInfo<int> UpdateFacturaRefound(Factura item);
        public ResultInfo<int> DeleteFactura(int id);
    }
}