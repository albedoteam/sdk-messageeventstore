namespace AlbedoTeam.Sdk.EventStore.Configuration
{
    internal class EventStoreOptions : IEventStoreOptions
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}