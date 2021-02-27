using AlbedoTeam.Sdk.DataLayerAccess;
using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
using AlbedoTeam.Sdk.EventStore.Models;

namespace AlbedoTeam.Sdk.EventStore.Db
{
    public class EventStoreRepository : BaseRepositoryWithAccount<EventOcurred>, IEventStoreRepository
    {
        public EventStoreRepository(IBaseRepository<EventOcurred> baseRepository) : base(baseRepository)
        {
        }
    }
}