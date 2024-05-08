using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Helpers;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;
using Microsoft.Extensions.Configuration;

namespace DSM.ERPerfect.DAL
{
    public class FormaPagoRepository : IFormaPagoRepository
    {
        private readonly string conString;

        public FormaPagoRepository(IConfiguration config)
        {
            string cs = config.GetConnectionString(config["ActiveConnection"]);
            conString = cs;
        }

        #region QUERY's

        public ResultInfo<List<FormaPago>> GetFormasPago()
        {
            ResultInfo<List<FormaPago>> result = new ResultInfo<List<FormaPago>>();
            string procedure = "SP_QUERY_FormasPago";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FormaPagoRepository.GetFormasPago"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        //result.Errors.Add(new ResultError($"", false, "FormaPagoRepository.GetFormasPago"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<FormaPago>.Get(dbres);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FormaPagoRepository.GetFormasPago"));
            }

            return result;
        }

        public ResultInfo<FormaPago> GetFormaPagoById(int id)
        {
            ResultInfo<FormaPago> result = new ResultInfo<FormaPago>();
            string procedure = "SP_SEL_FormasPagoById";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdFormaPago = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FormaPagoRepository.GetFormaPagoById"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha encontrado ninguna forma de pago con el id: '{id}'", false, "FormaPagoRepository.GetFormaPagoById"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<FormaPago>.Get(dbres.Rows[0]);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FormaPagoRepository.GetFormaPagoById"));
            }

            return result;
        }

        #endregion

        #region CREATE, UPDATE, DELETE

        public ResultInfo<int> NewFormaPago(FormaPago item)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_NEW_FormaPago";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, item);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FormaPagoRepository.NewFormaPago"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido crear una forma de pago con los datos: '{JSONHelper.JsonSerializer(item)}'", false, "FormaPagoRepository.NewFormaPago"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FormaPagoRepository.NewFormaPago"));
            }

            return result;
        }

        public ResultInfo<int> UpdateFormaPago(FormaPago item)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_FormaPago";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, item);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FormaPagoRepository.UpdateFormaPago"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido actualizar la forma de pago con los datos: '{JSONHelper.JsonSerializer(item)}'", false, "FormaPagoRepository.UpdateFormaPago"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FormaPagoRepository.UpdateFormaPago"));
            }

            return result;
        }

        public ResultInfo<int> DisabledFormaPago(int id)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_DisabledFormaPago";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdFormaPago = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FormaPagoRepository.DisabledFormaPago"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido deshabilitar la forma de pago con id: '{id}'", false, "FormaPagoRepository.DisabledFormaPago"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FormaPagoRepository.DisabledFormaPago"));
            }

            return result;
        }

        public ResultInfo<int> EnabledFormaPago(int id)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_EnabledFormaPago";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdFormaPago = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FormaPagoRepository.EnabledFormaPago"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido habilitar la forma de pago con id: '{id}'", false, "FormaPagoRepository.EnabledFormaPago"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FormaPagoRepository.EnabledFormaPago"));
            }

            return result;
        }

        public ResultInfo<int> DeleteFormaPago(int id)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_DEL_FormaPago";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdFormaPago = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FormaPagoRepository.DeleteFormaPago"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido eliminar la forma de pago con id: '{id}'", false, "FormaPagoRepository.DeleteFormaPago"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FormaPagoRepository.DeleteFormaPago"));
            }

            return result;
        }

        #endregion

    }
}
