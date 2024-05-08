using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Helpers;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;
using Microsoft.Extensions.Configuration;

namespace DSM.ERPerfect.DAL
{
    public class ServicioRepository : IServicioRepository
    {
        private readonly string conString;

        public ServicioRepository(IConfiguration config)
        {
            string cs = config.GetConnectionString(config["ActiveConnection"]);
            conString = cs;
        }

        #region QUERY's

        public ResultInfo<List<Servicio>> GetServicios()
        {
            ResultInfo<List<Servicio>> result = new ResultInfo<List<Servicio>>();
            string procedure = "SP_QUERY_GetServicios";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "ServicioRepository.GetServicios"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        //result.Errors.Add(new ResultError($"", false, "ServicioRepository.GetServicios"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<Servicio>.Get(dbres);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "ServicioRepository.GetServicios"));
            }

            return result;
        }

        public ResultInfo<Servicio> GetServicioById(int id)
        {
            ResultInfo<Servicio> result = new ResultInfo<Servicio>();
            string procedure = "SP_SEL_GetServicioById";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdServicio = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "ServicioRepository.GetServicioById"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha encontrado ningún Servicio con el id: '{id}'", false, "ServicioRepository.GetServicioById"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<Servicio>.Get(dbres.Rows[0]);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "ServicioRepository.GetServicioById"));
            }

            return result;
        }

        #endregion

        #region CREATE, UPDATE, DELETE

        public ResultInfo<int> NewServicio(Servicio item)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_NEW_Servicio";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, item);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "ServicioRepository.NewServicio"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido añadir ningún servicio con los datos: '{JSONHelper.JsonSerializer(item)}'", false, "ServicioRepository.NewServicio"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "ServicioRepository.NewServicio"));
            }

            return result;
        }

        public ResultInfo<int> UpdateServicio(Servicio item)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_Servicio";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, item);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "ServicioRepository.UpdateServicio"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido actualizar ningún servicio con los datos: '{JSONHelper.JsonSerializer(item)}'", false, "ServicioRepository.UpdateServicio"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "ServicioRepository.UpdateServicio"));
            }

            return result;
        }

        public ResultInfo<int> DisabledServicio(int id)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_DisabledServicio";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdServicio = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "ServicioRepository.DisabledServicio"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido deshabilitar ningún servicio con el id: '{id}'", false, "ServicioRepository.DisabledServicio"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "ServicioRepository.DisabledServicio"));
            }

            return result;
        }

        public ResultInfo<int> EnabledServicio(int id)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_EnabledServicio";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdServicio = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "ServicioRepository.EnabledServicio"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido habilitar ningún servicio con el id: '{id}'", false, "ServicioRepository.EnabledServicio"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "ServicioRepository.EnabledServicio"));
            }

            return result;
        }

        public ResultInfo<int> DeleteServicio(int id)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_DEL_Servicio";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdServicio = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "ServicioRepository.DeleteServicio"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido eliminar ningún servicio con el id: '{id}'", false, "ServicioRepository.DeleteServicio"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "ServicioRepository.DeleteServicio"));
            }

            return result;
        }

        #endregion

    }
}
