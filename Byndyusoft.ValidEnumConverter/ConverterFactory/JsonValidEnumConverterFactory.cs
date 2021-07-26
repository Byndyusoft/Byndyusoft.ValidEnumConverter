namespace Byndyusoft.ValidEnumConverter.ConverterFactory
{
    using System;
    using System.Reflection;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Converters;

    public class JsonValidEnumConverterFactory : JsonConverterFactory
    {
        private readonly JsonStringEnumConverter _innerFactory;

        public JsonValidEnumConverterFactory(JsonStringEnumConverter innerFactory)
        {
            _innerFactory = innerFactory ?? throw new ArgumentNullException(nameof(innerFactory));
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