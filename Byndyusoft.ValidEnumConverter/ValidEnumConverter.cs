namespace Byndyusoft.ValidEnumConverter
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using FrontOfficeApi.Converters.CustomConverterFactory;

    public static  class ValidEnumConverter
    {
        public static void AddValidEnumConverter(this IList<JsonConverter> converters)
        {
            converters.Add(new ValidJsonConverterFactory(new JsonStringEnumConverter()));
        }
    }
}