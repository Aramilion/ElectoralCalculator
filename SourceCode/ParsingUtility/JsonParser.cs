using Newtonsoft.Json;

namespace ParsingUtility
{
    public static class JsonParser
    {
        public static T GetData<T>(string jsonData)
        {
            return JsonConvert.DeserializeObject<T>(jsonData);
        }
    }
}
