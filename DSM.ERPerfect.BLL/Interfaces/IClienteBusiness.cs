using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

namespace DSM.ERPerfect.BLL.Interfaces
{
    public interface IClienteBusiness
    {
        public ResultInfo<List<Cliente>> GetClientes();
        public ResultInfo<Cliente> GetClienteById(int id);
        public ResultInfo<int> NewCliente(Cliente item);
        public ResultInfo<int> UpdateCliente(Cliente item);
        public ResultInfo<int> DisabledCliente(int id);
        public ResultInfo<int> EnabledCliente(int id);
    }
}