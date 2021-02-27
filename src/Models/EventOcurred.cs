using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
using AlbedoTeam.Sdk.DataLayerAccess.Attributes;
using AlbedoTeam.Sdk.EventStore.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace AlbedoTeam.Sdk.EventStore.Models
{
    [BsonCollection("EventStore")]
    public class EventOcurred : DocumentWithAccount
    {
        public string EventType { get; set; }

        [BsonSerializer(typeof(DynamicSerializer))]
        public dynamic Message { get; set; }

        public EventAuditMetadata Metadata { get; set; }
    }
}