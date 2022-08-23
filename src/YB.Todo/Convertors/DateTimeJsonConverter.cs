using System.Text.Json.Serialization;
using System;
using System.Globalization;
using System.Text.Json;

namespace YB.Todo.Convertors
{
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                DateTime.ParseExact(reader.GetString()!,
                    "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

        public override void Write(
            Utf8JsonWriter writer,
            DateTime dateTimeValue,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(dateTimeValue.ToString(
                    "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture));
    }
}
