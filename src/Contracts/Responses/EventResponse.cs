namespace AlbedoTeam.Sdk.EventStore.Contracts.Responses
{
    public class EventResponse
    {
        private string EventType { get; set; }
        private dynamic Message { get; set; }
        private EventMetadataResponse Metadata { get; set; }
    }
}