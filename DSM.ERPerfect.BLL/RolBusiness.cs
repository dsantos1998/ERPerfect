using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.DAL.Interfaces;

namespace DSM.ERPerfect.BLL
{
    public class RolBusiness : IRolBusiness
    {
        private IRolRepository _rolRepository { get; init; }

        public RolBusiness(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        #region Métodos públicos



        #endregion

        #region Métodos privados



        #endregion

    }
}
