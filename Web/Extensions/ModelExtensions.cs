using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Codell.Pies.Web.Extensions
{
    public static class ModelExtensions
    {
        public static string ToJson(this object model)
        {
            var serializeObject = JsonConvert.SerializeObject(model, new JavaScriptDateTimeConverter());
            return serializeObject;
        }

        public static TModel FromJson<TModel>(this string json)
        {
            return JsonConvert.DeserializeObject<TModel>(json);
        }       
    }
}