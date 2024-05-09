using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

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

        public ResultInfo<List<Factura>> GetFacturas()
        {
            return _facturaRepository.GetFacturas();
        }

        public ResultInfo<List<Factura>> GetFacturasPending()
        {
            return _facturaRepository.GetFacturas();
        }

        public ResultInfo<Factura> GetFacturaById(int id)
        {
            return _facturaRepository.GetFacturaById(id);
        }

        public ResultInfo<List<Factura>> GetFacturasByIdCliente(int idCliente)
        {
            return _facturaRepository.GetFacturasByIdCliente(idCliente);
        }

        public ResultInfo<int> NewFactura(Factura item)
        {
            return _facturaRepository.NewFactura(item);
        }

        public ResultInfo<int> UpdateFacturaPaid(Factura item)
        {
            return _facturaRepository.UpdateFacturaPaid(item);
        }

        public ResultInfo<int> UpdateFacturaRefound(Factura item)
        {
            return _facturaRepository.UpdateFacturaRefound(item);
        }

        public ResultInfo<int> DeleteFactura(int id)
        {
            return _facturaRepository.DeleteFactura(id);
        }


        #endregion

        #region Métodos privados



        #endregion

    }
}
