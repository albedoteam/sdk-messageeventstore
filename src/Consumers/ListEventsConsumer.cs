using System.Threading.Tasks;
using AlbedoTeam.Sdk.EventStore.Contracts.Requests;
using AlbedoTeam.Sdk.EventStore.Contracts.Responses;
using AlbedoTeam.Sdk.EventStore.Db;
using AlbedoTeam.Sdk.EventStore.Mappers;
using AlbedoTeam.Sdk.EventStore.Models;
using MassTransit;
using MongoDB.Driver;

namespace AlbedoTeam.Sdk.EventStore.Consumers
{
    public class ListEventsConsumer : IConsumer<ListEvents>
    {
        private readonly IMessageMapper _mapper;
        private readonly IEventStoreRepository _repository;

        public ListEventsConsumer(IEventStoreRepository repository, IMessageMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<ListEvents> context)
        {
            var page = context.Message.Page > 0 ? context.Message.Page : 1;
            var pageSize = context.Message.PageSize <= 1 ? 1 : context.Message.PageSize;
            var filters = CreateFilters(context);
            var sortBy = CreateSorting();

            var (totalPages, events) = await _repository.QueryByPage(
                context.Message.AccountId,
                page,
                pageSize,
                filters,
                sortBy);

            await context.RespondAsync<ListEventsResponse>(new
            {
                context.Message.Page,
                context.Message.PageSize,
                RecordsInPage = events.Count,
                TotalPages = totalPages,
                FilterBy = context.Message.EventType,
                Items = _mapper.MapModelToResponse(events)
            });
        }

        private static SortDefinition<EventOcurred> CreateSorting()
        {
            return Builders<EventOcurred>.Sort.Ascending(e => e.Metadata.SentTime);
        }

        private static FilterDefinition<EventOcurred> CreateFilters(ConsumeContext<ListEvents> context)
        {
            var requiredFilters = Builders<EventOcurred>.Filter.And(
                Builders<EventOcurred>.Filter.Gte(e => e.Metadata.SentTime, context.Message.Since));

            if (!string.IsNullOrWhiteSpace(context.Message.EventType))
                requiredFilters &= Builders<EventOcurred>.Filter.Eq(e => e.EventType, context.Message.EventType);

            var filters = Builders<EventOcurred>.Filter.And(
                context.Message.ShowDeleted
                    ? Builders<EventOcurred>.Filter.Empty
                    : Builders<EventOcurred>.Filter.Eq(a => a.IsDeleted, false),
                requiredFilters);

            return filters;
        }
    }
}