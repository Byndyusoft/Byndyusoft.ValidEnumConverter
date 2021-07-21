using System.Collections.Generic;

namespace FrontOfficeApi.Converters.CustomConverterFactory
{
    using System;
    using System.Reflection;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using CustomConverters;

    public class ValidJsonConverterFactory : JsonConverterFactory
    {
        private readonly JsonConverterFactory _innerFactory;

        public ValidJsonConverterFactory(JsonConverterFactory innerFactory)
        {
            _innerFactory = innerFactory ?? throw new ArgumentNullException(nameof(innerFactory));
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsEnum;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonStringEnumConverter = (JsonStringEnumConverter) _innerFactory;
            var defaultConverter = jsonStringEnumConverter.CreateConverter(typeToConvert, options);

            var converter = (JsonConverter) Activator.CreateInstance(
                    typeof(ValidJsonConverterEnum<>).MakeGenericType(typeToConvert),
                    BindingFlags.Instance | BindingFlags.Public,
                    null,
                    new object[] {defaultConverter},
                    null);

            return converter;
        }
    }
}