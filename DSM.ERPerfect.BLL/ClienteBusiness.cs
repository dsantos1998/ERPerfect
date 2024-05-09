using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

namespace DSM.ERPerfect.BLL
{
    public class ClienteBusiness : IClienteBusiness
    {
        private IClienteRepository _clienteRepository { get; init; }

        public ClienteBusiness(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        #region Métodos públicos

        public ResultInfo<List<Cliente>> GetClientes()
        {
            return _clienteRepository.GetClientes();
        }

        public ResultInfo<Cliente> GetClienteById(int id)
        {
            return _clienteRepository.GetClienteById(id);
        }

        public ResultInfo<int> NewCliente(Cliente item)
        {
            return _clienteRepository.NewCliente(item);
        }

        public ResultInfo<int> UpdateCliente(Cliente item)
        {
            return _clienteRepository.UpdateCliente(item);
        }

        public ResultInfo<int> DisabledCliente(int id)
        {
            return _clienteRepository.DisabledCliente(id);
        }

        public ResultInfo<int> EnabledCliente(int id)
        {
            return _clienteRepository.EnabledCliente(id);
        }


        #endregion

        #region Métodos privados



        #endregion

    }
}
