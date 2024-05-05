using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.DAL.Interfaces;

namespace DSM.ERPerfect.BLL
{
    public class ParametroBusiness : IParametroBusiness
    {
        private IParametroRepository _parametroRepository { get; init; }

        public ParametroBusiness(IParametroRepository parametroRepository)
        {
            _parametroRepository = parametroRepository;
        }

        #region Métodos públicos



        #endregion

        #region Métodos privados



        #endregion

    }
}
