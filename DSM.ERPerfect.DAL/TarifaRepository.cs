using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Helpers;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;
using Microsoft.Extensions.Configuration;

namespace DSM.ERPerfect.DAL
{
    public class TarifaRepository : ITarifaRepository
    {
        private readonly string conString;

        public TarifaRepository(IConfiguration config)
        {
            string cs = config.GetConnectionString(config["ActiveConnection"]);
            conString = cs;
        }

        #region QUERY's

        public ResultInfo<List<Tarifa>> GetTarifas()
        {
            ResultInfo<List<Tarifa>> result = new ResultInfo<List<Tarifa>>();
            string procedure = "SP_QUERY_GetTarifas";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "TarifaRepository.GetTarifas"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        //result.Errors.Add(new ResultError($"", false, "TarifaRepository.GetTarifas"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<Tarifa>.Get(dbres);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "TarifaRepository.GetTarifas"));
            }

            return result;
        }

        public ResultInfo<Tarifa> GetTarifaById(int id)
        {
            ResultInfo<Tarifa> result = new ResultInfo<Tarifa>();
            string procedure = "SP_QUERY_GetTarifaById";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdTarifa = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "TarifaRepository.GetTarifaById"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No existe ninguna tarifa con el id: '{id}'", false, "TarifaRepository.GetTarifaById"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<Tarifa>.Get(dbres.Rows[0]);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "TarifaRepository.GetTarifaById"));
            }

            return result;
        }

        #endregion

        #region CREATE, UPDATE, DELETE

        public ResultInfo<int> NewTarifa(Tarifa item)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_NEW_Tarifa";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, item);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "TarifaRepository.NewTarifa"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido añadir la tarifa con los datos: '{JSONHelper.JsonSerializer(item)}'", false, "TarifaRepository.NewTarifa"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "TarifaRepository.NewTarifa"));
            }

            return result;
        }

        public ResultInfo<int> UpdateTarifa(Tarifa item)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_Tarifa";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, item);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "TarifaRepository.UpdateTarifa"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido actualizar la tarifa con los datos: '{JSONHelper.JsonSerializer(item)}'", false, "TarifaRepository.UpdateTarifa"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "TarifaRepository.UpdateTarifa"));
            }

            return result;
        }

        public ResultInfo<int> DisabledTarifa(int id)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_DisabledTarifa";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdTarifa = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "TarifaRepository.DisabledTarifa"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido deshabilitar la tarifa con el id: '{id}'", false, "TarifaRepository.DisabledTarifa"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "TarifaRepository.DisabledTarifa"));
            }

            return result;
        }

        public ResultInfo<int> EnabledTarifa(int id)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_EnabledTarifa";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdTarifa = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "TarifaRepository.EnabledTarifa"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido habilitar la tarifa con el id: '{id}'", false, "TarifaRepository.EnabledTarifa"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "TarifaRepository.EnabledTarifa"));
            }

            return result;
        }

        public ResultInfo<int> DeleteTarifa(int id)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_DEL_Tarifa";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdTarifa = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "TarifaRepository.DeleteTarifa"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido eliminar la tarifa con el id: '{id}'", false, "TarifaRepository.DeleteTarifa"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "TarifaRepository.DeleteTarifa"));
            }

            return result;
        }

        #endregion

    }
}
