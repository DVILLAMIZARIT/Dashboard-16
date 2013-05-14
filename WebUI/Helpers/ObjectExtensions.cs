using System;
using System.IO;
using Newtonsoft.Json;

namespace WebUI.Helpers
{
    public static class ObjectExtensions
    {
        public static String ToJson(this Object obj)
        {
            JsonSerializer json = JsonSerializer.Create(new JsonSerializerSettings());
            StringWriter writer = new StringWriter();
            json.Serialize(writer, obj);
            return writer.ToString();
        }
    }
}