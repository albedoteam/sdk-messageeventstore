namespace AlbedoTeam.Sdk.EventStore.Configuration
{
    public interface IEventStoreOptions
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}