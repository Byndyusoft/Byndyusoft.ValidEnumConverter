namespace Byndyusoft.ValidEnumConverter
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using ConverterFactory;
    using System.Text.Json;

    public static class ValidEnumConverterJsonConvertersExtensions
    {
        public static void AddValidEnumConverter(
            this IList<JsonConverter> converters,
            JsonNamingPolicy? namingPolicy = null,
            bool allowIntegerValues = true)
        {
            converters.Add(
                new JsonValidEnumConverterFactory(new JsonStringEnumConverter(namingPolicy, allowIntegerValues)));
        }
    }
}