using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.DAL.Interfaces;

namespace DSM.ERPerfect.BLL
{
    public class TarifaBusiness : ITarifaBusiness
    {
        private ITarifaRepository _tarifaRepository { get; init; }

        public TarifaBusiness(ITarifaRepository tarifaRepository)
        {
            _tarifaRepository = tarifaRepository;
        }

        #region Métodos públicos



        #endregion

        #region Métodos privados



        #endregion

    }
}
