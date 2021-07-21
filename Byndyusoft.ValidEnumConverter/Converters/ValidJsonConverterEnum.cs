namespace FrontOfficeApi.Converters.CustomConverters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class ValidJsonConverterEnum<T> : JsonConverter<T>
            where T : struct, Enum
    {
        private readonly JsonConverter<T> _defaultJsonConverter;

        public ValidJsonConverterEnum(JsonConverter<T> defaultConverter)
        {
            _defaultJsonConverter = defaultConverter;
        }

        private int? GetNumber(Utf8JsonReader reader)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                {
                    var value = reader.GetString();
                    if (int.TryParse(value, out int result))
                    {
                        return result;
                    }

                    break;
                }
                case JsonTokenType.Number when reader.TryGetInt32(out int result):
                    return result;
            }

            return null;
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var number = GetNumber(reader);

            if (number != null && !Enum.IsDefined(typeof(T), number))
            {
                throw new JsonException();
            }

            return _defaultJsonConverter.Read(ref reader, typeToConvert, options);
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            _defaultJsonConverter.Write(writer, value, options);
        }
    }
}