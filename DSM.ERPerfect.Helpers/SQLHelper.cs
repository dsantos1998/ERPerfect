using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace DSM.ERPerfect.Helpers
{

    public class SQLHelper : IDisposable
    {
        private string connectionString;
        SqlConnection con;
        public string LastErrorString { get; private set; }

        public SQLHelper(string connectionString, bool desencriptar = true)
        {
            Inicializar(connectionString, desencriptar);
        }

        private void Inicializar(string cs, bool desencriptar)
        {
            if (desencriptar)
            {
                string[] csSplit = cs.Split(';');
                connectionString = cs;
            }
            else
            {
                connectionString = cs;
            }

            con = new SqlConnection(connectionString);
            con.Open();
        }

        public DataTable ExecuteProcedure(string procedure, object parametros = null, int timeout = 0)
        {
            LastErrorString = string.Empty;
            DataTable res;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand comando = new SqlCommand();
                    comando.Connection = con;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = procedure;
                    comando.CommandTimeout = timeout;
                    if (parametros != null) comando.Parameters.AddRange(GenerateParameters(parametros).ToArray());

                    SqlDataAdapter da = new SqlDataAdapter(comando);
                    DataTable dtRes = new DataTable();
                    da.Fill(dtRes);
                    res = dtRes;
                }
                catch (Exception ex)
                {
                    LastErrorString = ex.Message;
                    res = null;
                }
            }
            return res;
        }

        private List<SqlParameter> GenerateParameters(object obj)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (PropertyInfo item in obj.GetType().GetProperties())
                parameters.Add(new SqlParameter(item.Name, item.GetValue(obj, null)));

            return parameters;
        }

        public void Dispose()
        {
            con.Close();
            con.Dispose();
            con = null;
            GC.Collect();
        }
    }
}