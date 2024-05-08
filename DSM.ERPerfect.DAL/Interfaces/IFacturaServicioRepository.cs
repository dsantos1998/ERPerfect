using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

namespace DSM.ERPerfect.DAL.Interfaces
{
    public interface IFacturaServicioRepository
    {
        public ResultInfo<List<FacturaServicio>> GetFacturasServicios();
        public ResultInfo<List<FacturaServicio>> GetFacturaServicioByIdFactura(int idFactura);
        public ResultInfo<List<FacturaServicio>> GetFacturaServicioByIdServicio(int idServicio);
        public ResultInfo<FacturaServicio> GetFacturasServiciosById(int id);
        public ResultInfo<int> NewFacturaServicio(FacturaServicio item);
        public ResultInfo<int> DeleteFacturaServicio(FacturaServicio item);
    }
}