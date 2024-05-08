using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Helpers;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;
using Microsoft.Extensions.Configuration;

namespace DSM.ERPerfect.DAL
{
    public class FacturaServicioRepository : IFacturaServicioRepository
    {
        private readonly string conString;

        public FacturaServicioRepository(IConfiguration config)
        {
            string cs = config.GetConnectionString(config["ActiveConnection"]);
            conString = cs;
        }

        #region QUERY's

        public ResultInfo<List<FacturaServicio>> GetFacturasServicios()
        {
            ResultInfo<List<FacturaServicio>> result = new ResultInfo<List<FacturaServicio>>();
            string procedure = "SP_QUERY_FacturasServicios";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FacturaServicioRepository.GetAllClientes"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        //result.Errors.Add(new ResultError($"", false, "FacturaServicioRepository.GetAllClientes"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<FacturaServicio>.Get(dbres);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FacturaServicioRepository.GetAllClientes"));
            }

            return result;
        }

        public ResultInfo<List<FacturaServicio>> GetFacturaServicioByIdFactura(int idFactura)
        {
            ResultInfo<List<FacturaServicio>> result = new ResultInfo<List<FacturaServicio>>();
            string procedure = "SP_QUERY_FacturasServiciosByIdFactura";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdFactura = idFactura });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FacturaServicioRepository.GetFacturaServicioByIdFactura"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No existe ninguna FacturaServicio con el Idfactura: '{idFactura}'", false, "FacturaServicioRepository.GetFacturaServicioByIdFactura"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<FacturaServicio>.Get(dbres);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FacturaServicioRepository.GetFacturaServicioByIdFactura"));
            }

            return result;
        }

        public ResultInfo<List<FacturaServicio>> GetFacturaServicioByIdServicio(int idServicio)
        {
            ResultInfo<List<FacturaServicio>> result = new ResultInfo<List<FacturaServicio>>();
            string procedure = "SP_QUERY_FacturasServiciosByIdServicio";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdServicio = idServicio });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FacturaServicioRepository.GetFacturaServicioByIdServicio"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No existe ninguna FacturaServicio con el IdServicio: '{idServicio}'", false, "FacturaServicioRepository.GetFacturaServicioByIdServicio"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<FacturaServicio>.Get(dbres);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FacturaServicioRepository.GetFacturaServicioByIdServicio"));
            }

            return result;
        }

        public ResultInfo<FacturaServicio> GetFacturasServiciosById(int id)
        {
            ResultInfo<FacturaServicio> result = new ResultInfo<FacturaServicio>();
            string procedure = "SP_QUERY_FacturasServiciosById";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdServicio = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FacturaServicioRepository.GetFacturasServiciosById"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No existe ninguna FacturaServicio con el Id: '{id}'", false, "FacturaServicioRepository.GetFacturasServiciosById"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<FacturaServicio>.Get(dbres.Rows[0]);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FacturaServicioRepository.GetFacturasServiciosById"));
            }

            return result;
        }

        #endregion

        #region CREATE, UPDATE, DELETE

        public ResultInfo<int> NewFacturaServicio(FacturaServicio item)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_NEW_FacturasServicios";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, item);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FacturaServicioRepository.NewFacturaServicio"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido crear una FacturaServicio con los datos: '{JSONHelper.JsonSerializer(item)}'", false, "FacturaServicioRepository.NewFacturaServicio"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FacturaServicioRepository.NewFacturaServicio"));
            }

            return result;
        }

        public ResultInfo<int> DeleteFacturaServicio(FacturaServicio item)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_DeleteFacturasServicios";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, item);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FacturaServicioRepository.NewFacturaServicio"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido eliminar una FacturaServicio con los datos: '{JSONHelper.JsonSerializer(item)}'", false, "FacturaServicioRepository.NewFacturaServicio"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FacturaServicioRepository.NewFacturaServicio"));
            }

            return result;
        }

        #endregion

    }
}
