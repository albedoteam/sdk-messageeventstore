using System.Collections.Generic;
using System.Linq;
using AlbedoTeam.Sdk.EventStore.Contracts.Responses;
using AlbedoTeam.Sdk.EventStore.Models;
using AutoMapper;
using MassTransit.Audit;

namespace AlbedoTeam.Sdk.EventStore.Mappers
{
    public class MessageMapper : IMessageMapper
    {
        private readonly IMapper _mapper;

        public MessageMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MessageAuditMetadata, EventAuditMetadata>().ReverseMap();
                cfg.CreateMap<EventOcurred, EventResponse>().ReverseMap();
                cfg.CreateMap<EventAuditMetadata, EventMetadataResponse>().ReverseMap();
            });

            _mapper = config.CreateMapper();
        }

        public EventAuditMetadata MapMessageAuditMetadataToModel(MessageAuditMetadata message)
        {
            return _mapper.Map<MessageAuditMetadata, EventAuditMetadata>(message);
        }

        public List<EventResponse> MapModelToResponse(IReadOnlyList<EventOcurred> toList)
        {
            return _mapper.Map<List<EventOcurred>, List<EventResponse>>(toList.ToList());
        }
    }
}