using System.Data;

namespace DSM.ERPerfect.Helpers
{
    public class DataRowHelper<T> where T : class
    {
        private static object Get(DataRow dr, Type type, string entityName)
        {
            var instancia = Activator.CreateInstance(type);
            foreach (var p in type.GetProperties())
            {
                if (dr.Table.Columns[entityName + p.Name] == null)
                    continue;

                var ovalue = dr[entityName + p.Name];
                var propType = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
                p.SetValue(instancia, ovalue == DBNull.Value ? null : Convert.ChangeType(ovalue, propType), null);
            }
            return instancia;
        }

        public static T Get(DataRow dr)
        {
            var type = typeof(T);

            if (type == typeof(string))
            {
                return (T)dr[0];
            }

            var instancia = Activator.CreateInstance(type);
            foreach (var p in type.GetProperties())
            {

                if (dr.Table.Columns[p.Name] == null)
                    continue;

                var ovalue = dr[p.Name];
                var propType = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
                p.SetValue(instancia, ovalue == DBNull.Value ? null : Convert.ChangeType(ovalue, propType), null);

            }
            return (T)instancia;
        }

        public static List<T> Get(DataTable dt)
        {
            //return (from DataRow dr in dt.Rows select Get(dr)).ToList();
            Type type = typeof(T);
            List<T> ret = new List<T>();

            foreach (DataRow dr in dt.Rows)
                ret.Add(Get(dr));

            return ret;
        }

        public static U Get<U>(DataRow dr, string column)
        {
            var ovalue = dr[column];
            var type = typeof(U);
            return SafeValue<U>(ovalue, type);
        }

        private static U SafeValue<U>(object ovalue, Type type)
        {
            var t = Nullable.GetUnderlyingType(type) ?? type;
            var safeValue = (ovalue == DBNull.Value) ? null : Convert.ChangeType(ovalue, t);
            return (U)safeValue;
        }
    }
}