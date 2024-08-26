using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gurung.Wrapper.Wrapper
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class ResponseWrapperJsonConverter : JsonConverter<ResponseWrapper>
    {
        public override ResponseWrapper Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException("Deserialization is not implemented.");
        }

        public override void Write(Utf8JsonWriter writer, ResponseWrapper value, JsonSerializerOptions options)
        {
            // Get the naming policy from the options
            JsonNamingPolicy? namingPolicy = options.PropertyNamingPolicy;
            bool useCamelCase = namingPolicy == JsonNamingPolicy.CamelCase;

            writer.WriteStartObject();

            //writer.WriteBoolean("Success", value.Success);
            WriteProperty(writer, nameof(value.Success), value.Success, useCamelCase);

            WriteProperty(writer, nameof(value.Message), value.Message, useCamelCase);

            WriteProperty(writer, nameof(value.PageNumber), value.PageNumber, useCamelCase);

            WriteProperty(writer, nameof(value.TotalPages), value.TotalPages, useCamelCase);

            WriteProperty(writer, nameof(value.TotalPages), value.TotalPages, useCamelCase);

            WriteProperty(writer, nameof(value.TotalCount), value.TotalCount, useCamelCase);

            if (value.Errors is not null)
            {
                writer.WritePropertyName("Errors");
                JsonSerializer.Serialize(writer, value.Errors, options);
            }

            if (value.ValidationMessage is not null && value.ValidationMessage.Count > 0)
            {
                writer.WritePropertyName("ValidationMessage");
                JsonSerializer.Serialize(writer, value.ValidationMessage, options);
            }

            if (value.Data is not null)
            {
                writer.WritePropertyName("Data");
                JsonSerializer.Serialize(writer, value.Data, value.Data.GetType(), options);
            }

            writer.WriteEndObject();
        }

        private void WriteProperty(Utf8JsonWriter writer, string propertyName, object? value, bool useCamelCase)
        {
            if (value != null && (value is not string str || !string.IsNullOrEmpty(str)))
            {
                writer.WritePropertyName(useCamelCase ? ToCamelCase(propertyName) : propertyName);
                JsonSerializer.Serialize(writer, value, value.GetType(), new JsonSerializerOptions { PropertyNamingPolicy = useCamelCase ? JsonNamingPolicy.CamelCase : null });
            }
        }

        private string ToCamelCase(string name)
        {
            if (string.IsNullOrEmpty(name) || !char.IsUpper(name[0]))
                return name;

            return char.ToLower(name[0]) + name.Substring(1);
        }
    }

}
