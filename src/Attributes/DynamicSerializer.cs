using System;
using System.Text.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json.Linq;

namespace AlbedoTeam.Sdk.EventStore.Attributes
{
    public class DynamicSerializer : IBsonSerializer
    {
        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            object document;
            dynamic output;
            Type type;
            var currentBsonType = context.Reader.GetCurrentBsonType();

            switch (currentBsonType)
            {
                case BsonType.Document:
                    type = typeof(BsonDocument);
                    document = BsonSerializer.Deserialize(context.Reader, type) as BsonDocument;
                    output = JObject.Parse(document.ToJson(type));
                    break;
                case BsonType.Array:
                    type = typeof(BsonArray);
                    document = BsonSerializer.Deserialize(context.Reader, type) as BsonArray;
                    output = JArray.Parse(document.ToJson(type));
                    break;
                default:
                    throw new ApplicationException($"Invalid type {currentBsonType}");
            }

            return output;
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            var json = value == null ? "{}" : JsonSerializer.Serialize(value).Trim();

            if (StartsAndEnds(json, "[", "]"))
            {
                var array = BsonSerializer.Deserialize<BsonArray>(json);
                BsonSerializer.Serialize(context.Writer, typeof(BsonArray), array);
                return;
            }

            var document = BsonDocument.Parse(json);
            BsonSerializer.Serialize(context.Writer, typeof(BsonDocument), document);
        }

        public Type ValueType => typeof(object);

        private static bool StartsAndEnds(string value, string start, string end)
        {
            return value.StartsWith(start, StringComparison.InvariantCultureIgnoreCase) &&
                   value.EndsWith(end, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}