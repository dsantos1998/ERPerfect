using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

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

        public ResultInfo<List<FacturaServicio>> GetFacturasServicios()
        {
            return _facturaServicioRepository.GetFacturasServicios();
        }

        public ResultInfo<List<FacturaServicio>> GetFacturaServicioByIdFactura(int idFactura)
        {
            return _facturaServicioRepository.GetFacturaServicioByIdFactura(idFactura);
        }

        public ResultInfo<List<FacturaServicio>> GetFacturaServicioByIdServicio(int idServicio)
        {
            return _facturaServicioRepository.GetFacturaServicioByIdServicio(idServicio);
        }

        public ResultInfo<FacturaServicio> GetFacturasServiciosById(int id)
        {
            return _facturaServicioRepository.GetFacturasServiciosById(id);
        }

        public ResultInfo<int> NewFacturaServicio(FacturaServicio item)
        {
            return _facturaServicioRepository.NewFacturaServicio(item);
        }

        public ResultInfo<int> DeleteFacturaServicio(FacturaServicio item)
        {
            return _facturaServicioRepository.DeleteFacturaServicio(item);
        }


        #endregion

        #region Métodos privados



        #endregion

    }
}
