using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Helpers;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;
using Microsoft.Extensions.Configuration;

namespace DSM.ERPerfect.DAL
{
    public class RolRepository : IRolRepository
    {
        private readonly string conString;

        public RolRepository(IConfiguration config)
        {
            string cs = config.GetConnectionString(config["ActiveConnection"]);
            conString = cs;
        }

        #region QUERY's

        public ResultInfo<List<Rol>> GetRoles()
        {
            ResultInfo<List<Rol>> result = new ResultInfo<List<Rol>>();
            string procedure = "SP_QUERY_GetRoles";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "RolRepository.GetRoles"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        //result.Errors.Add(new ResultError($"", false, "RolRepository.GetRoles"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<Rol>.Get(dbres);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "RolRepository.GetRoles"));
            }

            return result;
        }

        public ResultInfo<Rol> GetRolById(int id)
        {
            ResultInfo<Rol> result = new ResultInfo<Rol>();
            string procedure = "SP_SEL_GetRolesById";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdRol = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "RolRepository.GetRolById"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No existe ningún rol con el id: '{id}'", false, "RolRepository.GetRolById"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<Rol>.Get(dbres.Rows[0]);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "RolRepository.GetRolById"));
            }

            return result;
        }

        public ResultInfo<Rol> GetRolByDescription(string description)
        {
            ResultInfo<Rol> result = new ResultInfo<Rol>();
            string procedure = "SP_SEL_GetRolesByDescripcion";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { Descripcion = description });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "RolRepository.GetRolByDescription"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No existe ningún rol con la descripcion: '{description}'", false, "RolRepository.GetRolByDescription"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<Rol>.Get(dbres.Rows[0]);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "RolRepository.GetRolByDescription"));
            }

            return result;
        }

        #endregion

        #region CREATE, UPDATE, DELETE

        public ResultInfo<int> NewRol(Rol item)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_NEW_Rol";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, item);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "RolRepository.NewRol"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido añadir el rol con los datos: '{JSONHelper.JsonSerializer(item)}'", false, "RolRepository.NewRol"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "RolRepository.NewRol"));
            }

            return result;
        }

        public ResultInfo<int> UpdateRol(Rol item)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_Rol";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, item);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "RolRepository.UpdateRol"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido actualizar el rol con los datos: '{JSONHelper.JsonSerializer(item)}'", false, "RolRepository.UpdateRol"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "RolRepository.UpdateRol"));
            }

            return result;
        }

        public ResultInfo<int> DisabledRol(int id)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_DisabledRol";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdRol = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "RolRepository.DisabledRol"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido deshabilitar el rol con el id: '{id}'", false, "RolRepository.DisabledRol"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "RolRepository.DisabledRol"));
            }

            return result;
        }

        public ResultInfo<int> EnabledRol(int id)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_EnabledRol";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdRol = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "RolRepository.EnabledRol"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido habilitar el rol con el id: '{id}'", false, "RolRepository.EnabledRol"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "RolRepository.EnabledRol"));
            }

            return result;
        }

        public ResultInfo<int> DeleteRol(int id)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_EnabledRol";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdRol = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "RolRepository.DeleteRol"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido eliminar el rol con el id: '{id}'", false, "RolRepository.DeleteRol"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "RolRepository.DeleteRol"));
            }

            return result;
        }

        #endregion

    }
}
