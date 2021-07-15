namespace FrontOfficeApi.Converters.CustomConverters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class JsonCustomConverterEnum<T> : JsonConverter<T>
            where T : struct, Enum
    {
        private JsonConverter<T> _jsonConverter;

        public JsonCustomConverterEnum(JsonConverter<T> defaultConverter)
        {
            _jsonConverter = defaultConverter;
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

            if (!number.HasValue)
            {
                var enumString = reader.GetString();
                if (!Enum.TryParse(enumString, out T _)
                    && !Enum.TryParse(enumString, ignoreCase: true, out T _))
                {
                    throw new JsonException();
                }
            }

            if (number.HasValue && !Enum.IsDefined(typeof(T), number))
            {
                throw new JsonException();
            }

            return _jsonConverter.Read(ref reader, typeToConvert, options);
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            _jsonConverter.Write(writer, value, options);
        }
    }
}