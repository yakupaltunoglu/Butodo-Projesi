using Newtonsoft.Json;

namespace ButodoProject.Core.Helper
{
    public static class JsonHelper
    {
        public static string ToJson(this object obj)
        {
            var jsonStr = JsonConvert.SerializeObject(obj);
            return jsonStr;
        }
        public static T ToObject<T>(this string value)
        {
            var obj = JsonConvert.DeserializeObject<T>(value);
            return obj;
        }
    }
}