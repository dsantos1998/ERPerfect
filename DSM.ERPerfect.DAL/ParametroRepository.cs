using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Helpers;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;
using Microsoft.Extensions.Configuration;

namespace DSM.ERPerfect.DAL
{
    public class ParametroRepository : IParametroRepository
    {
        private readonly string conString;

        public ParametroRepository(IConfiguration config)
        {
            string cs = config.GetConnectionString(config["ActiveConnection"]);
            conString = cs;
        }

        #region QUERY's

        public ResultInfo<List<Parametro>> GetParametros()
        {
            ResultInfo<List<Parametro>> result = new ResultInfo<List<Parametro>>();
            string procedure = "SP_QUERY_GetParametros";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "ParametroRepository.GetParametros"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        //result.Errors.Add(new ResultError($"", false, "ParametroRepository.GetParametros"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<Parametro>.Get(dbres);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "ParametroRepository.GetParametros"));
            }

            return result;
        }

        public ResultInfo<Parametro> GetParametroByCodigo(string codigo)
        {
            ResultInfo<Parametro> result = new ResultInfo<Parametro>();
            string procedure = "SP_SEL_GetParametrosByCodigo";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { Codigo = codigo });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "ParametroRepository.GetParametroByCodigo"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha encontrado ningún parámetro con el código: '{codigo}'", false, "ParametroRepository.GetParametroByCodigo"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<Parametro>.Get(dbres.Rows[0]);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "ParametroRepository.GetParametroByCodigo"));
            }

            return result;
        }

        public ResultInfo<Parametro> GetParametroById(int id)
        {
            ResultInfo<Parametro> result = new ResultInfo<Parametro>();
            string procedure = "SP_SEL_GetParametrosById";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdParametro = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "ParametroRepository.GetParametroById"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha encontrado ningún parámetro con el id: '{id}'", false, "ParametroRepository.GetParametroById"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<Parametro>.Get(dbres.Rows[0]);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "ParametroRepository.GetParametroById"));
            }

            return result;
        }

        #endregion

        #region CREATE, UPDATE, DELETE

        public ResultInfo<int> NewParametro(Parametro item)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_NEW_Parametro";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, item);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "ParametroRepository.NewParametro"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido crear un parámetro con los datos: '{JSONHelper.JsonSerializer(item)}'", false, "ParametroRepository.NewParametro"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "ParametroRepository.NewParametro"));
            }

            return result;
        }

        public ResultInfo<int> UpdateParametro(Parametro item)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_Parametro";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, item);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "ParametroRepository.UpdateParametro"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido actualizar un parámetro con los datos: '{JSONHelper.JsonSerializer(item)}'", false, "ParametroRepository.UpdateParametro"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "ParametroRepository.UpdateParametro"));
            }

            return result;
        }

        public ResultInfo<int> DisabledParametro(int id)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_DisabledParametro";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdParametro = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "ParametroRepository.DisabledParametro"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido deshabilitar el parámetro con id: '{id}'", false, "ParametroRepository.DisabledParametro"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "ParametroRepository.DisabledParametro"));
            }

            return result;
        }

        public ResultInfo<int> EnabledParametro(int id)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_EnabledParametro";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdParametro = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "ParametroRepository.EnabledParametro"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido habilitar el parámetro con id: '{id}'", false, "ParametroRepository.EnabledParametro"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "ParametroRepository.EnabledParametro"));
            }

            return result;
        }

        public ResultInfo<int> DeleteParametro(int id)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_DEL_Parametro";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdParametro = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "ParametroRepository.DeleteParametro"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido eliminar el parámetro con id: '{id}'", false, "ParametroRepository.DeleteParametro"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "ParametroRepository.DeleteParametro"));
            }

            return result;
        }

        #endregion

    }
}
