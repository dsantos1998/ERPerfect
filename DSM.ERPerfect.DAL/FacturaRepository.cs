using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Helpers;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;
using DSM.ERPerfect.Models.Statistics;
using Microsoft.Extensions.Configuration;

namespace DSM.ERPerfect.DAL
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly string conString;

        public FacturaRepository(IConfiguration config)
        {
            string cs = config.GetConnectionString(config["ActiveConnection"]);
            conString = cs;
        }

        #region QUERY's

        public ResultInfo<List<Factura>> GetFacturas()
        {
            ResultInfo<List<Factura>> result = new ResultInfo<List<Factura>>();
            string procedure = "SP_QUERY_GetFacturas";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FacturaRepository.GetFacturas"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        //result.Errors.Add(new ResultError($"", false, "FacturaRepository.GetFacturas"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<Factura>.Get(dbres);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FacturaRepository.GetFacturas"));
            }

            return result;
        }

        public ResultInfo<List<Factura>> GetFacturasPending()
        {
            ResultInfo<List<Factura>> result = new ResultInfo<List<Factura>>();
            string procedure = "SP_QUERY_GetFacturasPending";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FacturaRepository.GetFacturasPending"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        //result.Errors.Add(new ResultError($"", false, "FacturaRepository.GetFacturasPending"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<Factura>.Get(dbres);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FacturaRepository.GetFacturasPending"));
            }

            return result;
        }

        public ResultInfo<Factura> GetFacturaById(int id)
        {
            ResultInfo<Factura> result = new ResultInfo<Factura>();
            string procedure = "SP_SEL_GetFacturaById";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdFactura = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FacturaRepository.GetFacturaById"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha encontrado ninguna factura con id: {id}", false, "FacturaRepository.GetFacturaById"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<Factura>.Get(dbres.Rows[0]);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FacturaRepository.GetFacturaById"));
            }

            return result;
        }

        public ResultInfo<List<Factura>> GetFacturasByIdCliente(int idCliente)
        {
            ResultInfo<List<Factura>> result = new ResultInfo<List<Factura>>();
            string procedure = "SP_QUERY_GetFacturasByIdCliente";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdCliente = idCliente });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FacturaRepository.GetFacturasByIdCliente"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se han encotrado facturas para el cliente con id: {idCliente}", false, "FacturaRepository.GetFacturasByIdCliente"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<Factura>.Get(dbres);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FacturaRepository.GetFacturasByIdCliente"));
            }

            return result;
        }

        public ResultInfo<int> GetPendingBills()
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_SEL_GetPendingBills";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FacturaRepository.GetPendingBills"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        //result.Errors.Add(new ResultError($"No se ha podido eliminar la factura con el id: {id}", false, "FacturaRepository.DeleteFactura"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FacturaRepository.GetPendingBills"));
            }

            return result;
        }

        public ResultInfo<List<PaymentBills>> GetPaymentBills()
        {
            ResultInfo<List<PaymentBills>> result = new ResultInfo<List<PaymentBills>>();
            string procedure = "SP_QUERY_GetPaymentBills";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FacturaRepository.GetPaymentBills"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        //result.Errors.Add(new ResultError($"No se ha podido eliminar la factura con el id: {id}", false, "FacturaRepository.GetPaymentBills"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<PaymentBills>.Get(dbres);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FacturaRepository.GetPaymentBills"));
            }

            return result;
        }

        public ResultInfo<List<Top5Servicios>> GetTop5ServicioMes()
        {
            ResultInfo<List<Top5Servicios>> result = new ResultInfo<List<Top5Servicios>>();
            string procedure = "SP_QUERY_GetTop5ServicioMes";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FacturaRepository.GetTop5ServicioMes"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        //result.Errors.Add(new ResultError($"No se ha podido eliminar la factura con el id: {id}", false, "FacturaRepository.GetTop5ServicioMes"));
                    }
                    else
                    {
                        result.Content = DataRowHelper<Top5Servicios>.Get(dbres);
                    }
                    dbres?.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FacturaRepository.GetTop5ServicioMes"));
            }

            return result;
        }

        #endregion

        #region CREATE, UPDATE, DELETE

        public ResultInfo<int> NewFactura(Factura item)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_NEW_Factura";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, item);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FacturaRepository.NewFactura"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido crear una factura con los datos: {JSONHelper.JsonSerializer(item)}", false, "FacturaRepository.NewFactura"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FacturaRepository.NewFactura"));
            }

            return result;
        }

        public ResultInfo<int> UpdateFacturaPaid(Factura item)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_FacturaPaid";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, item);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FacturaRepository.UpdateFacturaPaid"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido cobrar la factura con los datos: {JSONHelper.JsonSerializer(item)}", false, "FacturaRepository.UpdateFacturaPaid"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FacturaRepository.UpdateFacturaPaid"));
            }

            return result;
        }

        public ResultInfo<int> UpdateFacturaRefound(Factura item)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_FacturaRefound";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, item);

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FacturaRepository.UpdateFacturaRefound"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido abonar la factura con los datos: {JSONHelper.JsonSerializer(item)}", false, "FacturaRepository.UpdateFacturaRefound"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FacturaRepository.UpdateFacturaRefound"));
            }

            return result;
        }

        public ResultInfo<int> DeleteFactura(int id)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_DEL_Factura";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { IdFactura = id });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FacturaRepository.DeleteFactura"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido eliminar la factura con el id: {id}", false, "FacturaRepository.DeleteFactura"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FacturaRepository.DeleteFactura"));
            }

            return result;
        }

        public ResultInfo<int> UpdateFactura(int idFactura, int idCliente, int idServicio, int idFormaPago)
        {
            ResultInfo<int> result = new ResultInfo<int>();
            string procedure = "SP_UPD_Factura";
            try
            {
                using (SQLHelper repo = new SQLHelper(conString, false))
                {
                    var dbres = repo.ExecuteProcedure(procedure, new { idFactura = idFactura, IdCliente = idCliente, idServicio = idServicio, IdFormaPago = idFormaPago });

                    if (!string.IsNullOrEmpty(repo.LastErrorString))
                    {
                        result.Errors.Add(new ResultError(repo.LastErrorString, false, "FacturaRepository.UpdateFactura"));
                    }
                    else if (dbres == null || dbres.Rows.Count == 0)
                    {
                        result.Errors.Add(new ResultError($"No se ha podido actualizar la factura con el id: {idFactura}", false, "FacturaRepository.UpdateFactura"));
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
                result.Errors.Add(new ResultError(ex.InnerException == null ? ex.Message : ex.InnerException.Message, true, "FacturaRepository.UpdateFactura"));
            }

            return result;
        }

        #endregion

    }
}
