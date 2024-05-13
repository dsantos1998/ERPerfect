using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Helpers;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;
using DSM.ERPerfect.Models.Queries;
using Microsoft.Extensions.Configuration;

namespace DSM.ERPerfect.DAL
{
    public class TarifaServicioRepository : ITarifaServicioRepository
    {
        private readonly string conString;

        public TarifaServicioRepository(IConfiguration config)
        {
            string cs = config.GetConnectionString(config["ActiveConnection"]);
            conString = cs;
        }

        #region QUERY's

        public ResultInfo<List<TarifaServicioQuery>> GetTarifaServicio()
        {
            ResultInfo<List<TarifaServicioQuery>> result = new ResultInfo<List<TarifaServicioQuery>>();
            string procedure = "SP_QUERY_GetTarifaServicio";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "TarifaServicioRepository.GetTarifaServicio"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        //result.Errors.Add(new ResultError($"", false, "TarifaServicioRepository.GetTarifaServicio"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<TarifaServicioQuery>.Get(dbres);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "TarifaServicioRepository.GetTarifaServicio"));
            }

            return result;
        }

        public ResultInfo<TarifaServicioQuery> GetTarifaServicioById(int id)
        {
            ResultInfo<TarifaServicioQuery> result = new ResultInfo<TarifaServicioQuery>();
            string procedure = "SP_SEL_GetTarifaServicioById";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdTarifaServicio = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "TarifaServicioRepository.GetTarifaServicioById"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No existe ningúna TarifaServicio con id: '{id}'", false, "TarifaServicioRepository.GetTarifaServicioById"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<TarifaServicioQuery>.Get(dbres.Rows[0]);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "TarifaServicioRepository.GetTarifaServicioById"));
            }

            return result;
        }

        public ResultInfo<List<TarifaServicioQuery>> GetTarifaServicioByIdTarifa(int id)
        {
            ResultInfo<List<TarifaServicioQuery>> result = new ResultInfo<List<TarifaServicioQuery>>();
            string procedure = "SP_QUERY_GetTarifaServicioByIdTarifa";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdTarifa = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "TarifaServicioRepository.GetTarifaServicioByIdTarifa"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No existe ningúna TarifaServicio con IdTarifa: '{id}'", false, "TarifaServicioRepository.GetTarifaServicioByIdTarifa"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<TarifaServicioQuery>.Get(dbres);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "TarifaServicioRepository.GetTarifaServicioByIdTarifa"));
            }

            return result;
        }

        public ResultInfo<List<TarifaServicioQuery>> GetTarifaServicioByIdServicio(int id)
        {
            ResultInfo<List<TarifaServicioQuery>> result = new ResultInfo<List<TarifaServicioQuery>>();
            string procedure = "SP_QUERY_GetTarifaServicioByIdServicio";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdServicio = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "TarifaServicioRepository.GetTarifaServicioByIdServicio"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No existe ningúna TarifaServicio con IdServicio: '{id}'", false, "TarifaServicioRepository.GetTarifaServicioByIdServicio"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<TarifaServicioQuery>.Get(dbres);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "TarifaServicioRepository.GetTarifaServicioByIdServicio"));
            }

            return result;
        }

        #endregion

        #region CREATE, UPDATE, DELETE

        public ResultInfo<int> NewTarifaServicio(TarifaServicio item)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_NEW_TarifaServicio";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, item);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "TarifaServicioRepository.NewTarifaServicio"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido añadir ninguna TarifaServicio con los datos: '{JSONHelper.JsonSerializer(item)}'", false, "TarifaServicioRepository.NewTarifaServicio"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "TarifaServicioRepository.NewTarifaServicio"));
            }

            return result;
        }

        public ResultInfo<int> UpdateTarifaServicio(TarifaServicio item)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_TarifaServicio";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, item);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "TarifaServicioRepository.UpdateTarifaServicio"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido actualizar ninguna TarifaServicio con los datos: '{JSONHelper.JsonSerializer(item)}'", false, "TarifaServicioRepository.UpdateTarifaServicio"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "TarifaServicioRepository.UpdateTarifaServicio"));
            }

            return result;
        }

        public ResultInfo<int> DisabledTarifaServicio(int id)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_DisabledTarifaServicio";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdTarifaServicio = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "TarifaServicioRepository.DisabledTarifaServicio"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido deshabilitar ninguna TarifaServicio con el Id: '{id}'", false, "TarifaServicioRepository.DisabledTarifaServicio"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "TarifaServicioRepository.DisabledTarifaServicio"));
            }

            return result;
        }

        public ResultInfo<int> EnabledTarifaServicio(int id)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_EnabledTarifaServicio";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdTarifaServicio = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "TarifaServicioRepository.EnabledTarifaServicio"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido habilitar ninguna TarifaServicio con el Id: '{id}'", false, "TarifaServicioRepository.EnabledTarifaServicio"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "TarifaServicioRepository.EnabledTarifaServicio"));
            }

            return result;
        }

        public ResultInfo<int> DeleteTarifaServicio(int id)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_DEL_TarifaServicio";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdTarifaServicio = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "TarifaServicioRepository.DeleteTarifaServicio"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido habilitar ninguna TarifaServicio con el Id: '{id}'", false, "TarifaServicioRepository.DeleteTarifaServicio"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "TarifaServicioRepository.DeleteTarifaServicio"));
            }

            return result;
        }

        #endregion

    }
}
