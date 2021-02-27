using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
using AlbedoTeam.Sdk.EventStore.Models;

namespace AlbedoTeam.Sdk.EventStore.Db
{
    public interface IEventStoreRepository : IBaseRepositoryWithAccount<EventOcurred>
    {
    }
}