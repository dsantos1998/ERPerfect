using DSM.ERPerfect.DAL.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DSM.ERPerfect.DAL
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string conString;

        public ClienteRepository(IConfiguration config)
        {
            string cs = config.GetConnectionString(config["ActiveConnection"]);
            conString = cs;
        }

        #region QUERY's

        //public ResultInfo<List<Cliente>> GetClientes()
        //{
        //    ResultInfo<List<Cliente>> result = new ResultInfo<List<Cliente>>();
        //    string procedure = "SP_QUERY_GetClientes";
        //    try
        //    {
        //        using (SQLHelper repo = new SQLHelper(conString, false))
        //        {
        //            var dbres = repo.ExecuteProcedure(procedure);

        //            if (!string.IsNullOrEmpty(repo.LastErrorString))
        //            {
        //                result.Errors.Add(new ResultError(repo.LastErrorString, false, "ClienteRepository.GetAllClientes"));
        //            }
        //            else if (dbres == null || dbres.Rows.Count == 0)
        //            {
        //                //result.Errors.Add(new ResultError($"", false, "ClienteRepository.GetAllClientes"));
        //            }
        //            else
        //            {
        //                result.Content = DataRowHelper<Cliente>.Get(dbres);
        //            }
        //            dbres?.Dispose();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "ClienteRepository.GetAllClientes"));
        //    }

        //    return result;
        //}

        #endregion

        #region CREATE, UPDATE, DELETE



        #endregion

    }
}
