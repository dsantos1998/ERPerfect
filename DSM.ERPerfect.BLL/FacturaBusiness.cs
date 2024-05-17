using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;
using DSM.ERPerfect.Models.Statistics;

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
            return _facturaRepository.GetFacturasPending();
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

        public ResultInfo<int> GetPendingBills()
        {
            return _facturaRepository.GetPendingBills();
        }

        public ResultInfo<List<PaymentBills>> GetPaymentBills()
        {
            ResultInfo<List<PaymentBills>> result = _facturaRepository.GetPaymentBills();

            if (!result.HasErrors)
            {
                if(result.Content != null)
                {
                    if(result.Content.Count > 0)
                    {
                        foreach (PaymentBills item in result.Content)
                        {
                            switch (item.Mes)
                            {
                                case 1:
                                    item.MesDescripcion = "Enero";
                                    break;
                                case 2:
                                    item.MesDescripcion = "Febrero";
                                    break;
                                case 3:
                                    item.MesDescripcion = "Marzo";
                                    break;
                                case 4:
                                    item.MesDescripcion = "Abril";
                                    break;
                                case 5:
                                    item.MesDescripcion = "Mayo";
                                    break;
                                case 6:
                                    item.MesDescripcion = "Junio";
                                    break;
                                case 7:
                                    item.MesDescripcion = "Julio";
                                    break;
                                case 8:
                                    item.MesDescripcion = "Agosto";
                                    break;
                                case 9:
                                    item.MesDescripcion = "Septiembre";
                                    break;
                                case 10:
                                    item.MesDescripcion = "Octubre";
                                    break;
                                case 11:
                                    item.MesDescripcion = "Noviembre";
                                    break;
                                case 12:
                                    item.MesDescripcion = "Diciembre";
                                    break;
                            }
                        }
                    }
                }
            }

            return result;
        }

        public ResultInfo<List<Top5Servicios>> GetTop5ServicioMes()
        {
            return _facturaRepository.GetTop5ServicioMes();
        }

        public ResultInfo<int> UpdateFactura(int idFactura, int idCliente, int idServicio, int idFormaPago)
        {
            return _facturaRepository.UpdateFactura(idFactura, idCliente, idServicio, idFormaPago);
        }

        #endregion

        #region Métodos privados



        #endregion

    }
}
