namespace Byndyusoft.ValidEnumConverter.Converters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class JsonValidEnumConverter<T> : JsonConverter<T>
        where T : struct, Enum
    {
        private readonly JsonConverter<T> _defaultJsonConverter;

        public JsonValidEnumConverter(JsonConverter<T> defaultConverter)
        {
            _defaultJsonConverter = defaultConverter;
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = _defaultJsonConverter.Read(ref reader, typeToConvert, options);

            if (Enum.IsDefined(typeof(T), value) == false)
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