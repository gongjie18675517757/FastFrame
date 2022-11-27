using Newtonsoft.Json;


namespace FastFrame.Infrastructure
{
    /// <summary>
    /// 默认接口JSON转换器
    /// </summary>
    /// <typeparam name="TInterface"></typeparam>
    /// <typeparam name="TClass"></typeparam>
    public class NewtonsoftDefaultInterfaceConvert<TInterface, TClass> : JsonConverter<TInterface>
        where TClass : class, TInterface, new()
    {
        public override bool CanWrite => false;

        public override TInterface ReadJson(JsonReader reader, Type objectType, TInterface existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<Newtonsoft.Json.Linq.JObject>(reader).ToObject<TClass>();
        }

        public override void WriteJson(JsonWriter writer, TInterface value, JsonSerializer serializer)
        { 
            serializer.Serialize(writer, value);
        }
    }

    //public class TextDefaultInterfaceConvert<TInterface, TClass> : System.Text.Json.Serialization.JsonConverter<TInterface>
    //where TClass : class, TInterface, new()
    //{
    //    public override TInterface Read(ref System.Text.Json.Utf8JsonReader reader, Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
    //    {
            
    //    }

    //    public override void Write(System.Text.Json.Utf8JsonWriter writer, TInterface value, System.Text.Json.JsonSerializerOptions options)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
