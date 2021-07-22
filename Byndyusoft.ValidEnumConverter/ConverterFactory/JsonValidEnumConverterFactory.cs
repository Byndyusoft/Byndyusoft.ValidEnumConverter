using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Byndyusoft.ValidEnumConverter.Converters;

namespace Byndyusoft.ValidEnumConverter.ConverterFactory
{
    public class JsonValidEnumConverterFactory : JsonConverterFactory
    {
        private readonly JsonStringEnumConverter _innerFactory;

        public JsonValidEnumConverterFactory(JsonNamingPolicy namingPolicy = null, bool allowIntegerValues = true)
        {
            _innerFactory = new JsonStringEnumConverter(namingPolicy, allowIntegerValues);
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsEnum;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var defaultConverter = _innerFactory.CreateConverter(typeToConvert, options);
            var converter = (JsonConverter) Activator.CreateInstance(
                typeof(JsonValidEnumConverter<>).MakeGenericType(typeToConvert),
                BindingFlags.Instance | BindingFlags.Public,
                null,
                new object[] {defaultConverter},
                null);

            return converter;
        }
    }
}