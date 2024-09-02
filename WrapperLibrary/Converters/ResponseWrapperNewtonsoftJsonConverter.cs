using Gurung.Wrapper.Wrapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gurung.Wrapper.Converters
{
    public class ResponseWrapperNewtonsoftJsonConverter : JsonConverter<ResponseWrapper>
    {
        public override ResponseWrapper ReadJson(JsonReader reader, Type objectType, ResponseWrapper existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Deserialization is not implemented.");
        }
        public override void WriteJson(JsonWriter writer, ResponseWrapper value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            var namingStrategy = (serializer.ContractResolver as DefaultContractResolver)?.NamingStrategy;
            WriteProperty(writer, nameof(value.Success), value.Success, namingStrategy, serializer);
            WriteProperty(writer, nameof(value.Message), value.Message, namingStrategy, serializer);

            if (value.IsPaged)
            {
                WriteProperty(writer, nameof(value.PageNumber), value.PageNumber, namingStrategy, serializer);
                WriteProperty(writer, nameof(value.TotalPages), value.TotalPages, namingStrategy, serializer);
                WriteProperty(writer, nameof(value.TotalCount), value.TotalCount, namingStrategy, serializer);
                WriteProperty(writer, nameof(value.PageSize), value.PageSize, namingStrategy, serializer);
            }

            if (value.Errors is not null && value.Errors.Count > 0)
            {
                writer.WritePropertyName(GetPropertyName(nameof(value.Errors), namingStrategy));
                serializer.Serialize(writer, value.Errors);
            }

            if (value.ValidationMessage is not null && value.ValidationMessage.Count > 0)
            {
                writer.WritePropertyName(GetPropertyName(nameof(value.ValidationMessage), namingStrategy));
                serializer.Serialize(writer, value.ValidationMessage);
            }

            if (value.IsPaged || value.ShowData)
            {
                writer.WritePropertyName(GetPropertyName(nameof(value.Data), namingStrategy));
                serializer.Serialize(writer, value.Data);
            }

            writer.WriteEndObject();
        }

        private void WriteProperty(JsonWriter writer, string propertyName, object value, NamingStrategy namingStrategy, JsonSerializer serializer)
        {
            if (value != null)
            {
                writer.WritePropertyName(GetPropertyName(propertyName, namingStrategy));
                serializer.Serialize(writer, value);
            }
        }

        private string GetPropertyName(string propertyName, NamingStrategy namingStrategy)
        {
            return namingStrategy != null ? namingStrategy.GetPropertyName(propertyName, false) : propertyName;
        }
    }
}
