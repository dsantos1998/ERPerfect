using DSM.ERPerfect.Models.Errors;
using Newtonsoft.Json;

namespace DSM.ERPerfect.Helpers
{
    public class JSONHelper
    {
        public static string JsonSerializer(object item)
        {
            return JsonConvert.SerializeObject(item, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include }); //Requiere la instalación del paquete de NuGet "Newtonsoft.Json"
        }

        public static ResultInfo<T> JsonDeserializer<T>(string json) where T : class
        {
            ResultInfo<T> result = new ResultInfo<T>();
            try
            {
                result.Content = JsonConvert.DeserializeObject<T>(json); //Requiere la instalación del paquete de NuGet "Newtonsoft.Json"
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ResultError("Failed to deserialize the JSON object.", true, "JSONHelper.JsonDeserializer"));
                result.Errors.Add(new ResultError($"{ex.Message}", true, "JSONHelper.JsonDeserializer"));
                result.Errors.Add(new ResultError($"{ex.InnerException}", true, "JSONHelper.JsonDeserializer"));
                result.Errors.Add(new ResultError($"{ex}", true, "JSONHelper.JsonDeserializer"));
            }

            return result;
        }
    }
}