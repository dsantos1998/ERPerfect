using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Helpers;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;
using Microsoft.Extensions.Configuration;

namespace DSM.ERPerfect.DAL
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string conString;

        public UsuarioRepository(IConfiguration config)
        {
            string cs = config.GetConnectionString(config["ActiveConnection"]);
            conString = cs;
        }

        #region QUERY's

        public ResultInfo<List<Usuario>> GetUsuarios()
        {
            ResultInfo<List<Usuario>> result = new ResultInfo<List<Usuario>>();
            string procedure = "SP_QUERY_GetUsuarios";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "UsuarioRepository.GetUsuarios"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        //result.Errors.Add(new ResultError($"", false, "UsuarioRepository.GetUsuarios"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<Usuario>.Get(dbres);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "UsuarioRepository.GetUsuarios"));
            }

            return result;
        }

        public ResultInfo<Usuario> GetUsuarioByRowGUID(Guid guid)
        {
            ResultInfo<Usuario> result = new ResultInfo<Usuario>();
            string procedure = "SP_SEL_GetUsuarioByRowGUID";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { RowGUID = guid });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "UsuarioRepository.GetUsuarioByRowGUID"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No existe ningún usuario con el RowGUID: '{guid}'", false, "UsuarioRepository.GetUsuarioByRowGUID"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<Usuario>.Get(dbres.Rows[0]);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "UsuarioRepository.GetUsuarioByRowGUID"));
            }

            return result;
        }

        public ResultInfo<Usuario> GetUsuarioByLogin(string login)
        {
            ResultInfo<Usuario> result = new ResultInfo<Usuario>();
            string procedure = "SP_SEL_GetUsuarioByLogin";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { Login = login });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "UsuarioRepository.GetUsuarioByLogin"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No existe ningún usuario con el login: '{login}'", false, "UsuarioRepository.GetUsuarioByLogin"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<Usuario>.Get(dbres.Rows[0]);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "UsuarioRepository.GetUsuarioByLogin"));
            }

            return result;
        }

        #endregion

        #region CREATE, UPDATE, DELETE

        public ResultInfo<int> NewUsuario(Usuario item)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_NEW_Usuario";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, item);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "UsuarioRepository.NewUsuario"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido crear el usuario con los siguientes datos: '{JSONHelper.JsonSerializer(item)}'", false, "UsuarioRepository.NewUsuario"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "UsuarioRepository.NewUsuario"));
            }

            return result;
        }

        public ResultInfo<int> UpdateUsuario(Usuario item)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_Usuario";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, item);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "UsuarioRepository.UpdateUsuario"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido actualizar el usuario con los siguientes datos: '{JSONHelper.JsonSerializer(item)}'", false, "UsuarioRepository.UpdateUsuario"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "UsuarioRepository.UpdateUsuario"));
            }

            return result;
        }

        public ResultInfo<int> DisabledUsuario(int idUsuario)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_DesactivarUsuario";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdUsuario = idUsuario });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "UsuarioRepository.DisabledUsuario"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido deshabilitar al usuario con id: '{idUsuario}'", false, "UsuarioRepository.DisabledUsuario"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "UsuarioRepository.DisabledUsuario"));
            }

            return result;
        }

        public ResultInfo<int> EnabledUsuario(int idUsuario)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_ActivarUsuario";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdUsuario = idUsuario });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "UsuarioRepository.EnabledUsuario"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido habilitar al usuario con id: '{idUsuario}'", false, "UsuarioRepository.EnabledUsuario"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "UsuarioRepository.EnabledUsuario"));
            }

            return result;
        }

        public ResultInfo<int> DeleteUsuario(int idUsuario)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_DEL_Usuario";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdUsuario = idUsuario });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "UsuarioRepository.DeleteUsuario"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido eliminar al usuario con id: '{idUsuario}'", false, "UsuarioRepository.DeleteUsuario"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "UsuarioRepository.DeleteUsuario"));
            }

            return result;
        }

        #endregion

    }
}
