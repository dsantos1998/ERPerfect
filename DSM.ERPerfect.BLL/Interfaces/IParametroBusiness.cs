using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

namespace DSM.ERPerfect.BLL.Interfaces
{
    public interface IParametroBusiness
    {
        public ResultInfo<List<Parametro>> GetParametros();
        public ResultInfo<Parametro> GetParametroByCodigo(string codigo);
        public ResultInfo<Parametro> GetParametroById(int id);
        public ResultInfo<int> NewParametro(Parametro item);
        public ResultInfo<int> UpdateParametro(Parametro item);
        public ResultInfo<int> DisabledParametro(int id);
        public ResultInfo<int> EnabledParametro(int id);
        public ResultInfo<int> DeleteParametro(int id);
    }
}