using System.Collections.Generic;
using AlbedoTeam.Sdk.EventStore.Contracts.Responses;
using AlbedoTeam.Sdk.EventStore.Models;
using MassTransit.Audit;

namespace AlbedoTeam.Sdk.EventStore.Mappers
{
    public interface IMessageMapper
    {
        EventAuditMetadata MapMessageAuditMetadataToModel(MessageAuditMetadata message);
        List<EventResponse> MapModelToResponse(IReadOnlyList<EventOcurred> toList);
    }
}