using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Byndyusoft.ValidEnumConverter.ConverterFactory;

namespace Byndyusoft.ValidEnumConverter
{
    public static class ValidEnumConverterJsonConvertersExtensions
    {
        public static void AddValidEnumConverter(
            this IList<JsonConverter> converters,
            JsonNamingPolicy? namingPolicy = null,
            bool allowIntegerValues = true)
        {
            converters.Add(new JsonValidEnumConverterFactory(namingPolicy, allowIntegerValues));
        }
    }
}