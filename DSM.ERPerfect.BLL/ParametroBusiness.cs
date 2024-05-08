using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

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

        public ResultInfo<List<Parametro>> GetParametros()
        {
            return _parametroRepository.GetParametros();
        }

        public ResultInfo<Parametro> GetParametroByCodigo(string codigo)
        {
            return _parametroRepository.GetParametroByCodigo(codigo);
        }

        public ResultInfo<Parametro> GetParametroById(int id)
        {
            return _parametroRepository.GetParametroById(id);
        }

        public ResultInfo<int> NewParametro(Parametro item)
        {
            return _parametroRepository.NewParametro(item);
        }

        public ResultInfo<int> UpdateParametro(Parametro item)
        {
            return _parametroRepository.UpdateParametro(item);
        }

        public ResultInfo<int> DisabledParametro(int id)
        {
            return _parametroRepository.DisabledParametro(id);
        }

        public ResultInfo<int> EnabledParametro(int id)
        {
            return _parametroRepository.EnabledParametro(id);
        }

        public ResultInfo<int> DeleteParametro(int id)
        {
            return _parametroRepository.DeleteParametro(id);
        }


        #endregion

        #region Métodos privados



        #endregion

    }
}
