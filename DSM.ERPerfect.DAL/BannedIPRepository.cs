using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Helpers;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;
using Microsoft.Extensions.Configuration;

namespace DSM.ERPerfect.DAL
{
    public class BannedIPRepository : IBannedIPRepository
    {
        private readonly string conString;

        public BannedIPRepository(IConfiguration config)
        {
            string cs = config.GetConnectionString(config["ActiveConnection"]);
            conString = cs;
        }

        #region QUERY's

        public ResultInfo<List<BannedIP>> GetBannedIPs()
        {
            ResultInfo<List<BannedIP>> result = new ResultInfo<List<BannedIP>>();
            string procedure = "SP_QUERY_GetBannedIPs";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "BannedIPRepository.GetBannedIPs"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        //result.Errors.Add(new ResultError($"", false, "ClienteRepository.GetClientes"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<BannedIP>.Get(dbres);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "BannedIPRepository.GetBannedIPs"));
            }

            return result;
        }

        #endregion

        #region CREATE, UPDATE, DELETE

        public ResultInfo<int> NewBannedIP(string ip)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_NEW_BannedIP";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IpAddress = ip });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "BannedIPRepository.NewBannedIP"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido añadir una BannedIP con la ip: '{ip}'", false, "BannedIPRepository.NewBannedIP"));
                    }
                    else
                    {
                        result.Content = Convert.ToInt32(dbres.Rows[0][0]);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "BannedIPRepository.NewBannedIP"));
            }

            return result;
        }

        #endregion

    }
}
