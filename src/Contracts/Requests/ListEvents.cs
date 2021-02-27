using System;

namespace AlbedoTeam.Sdk.EventStore.Contracts.Requests
{
    public interface ListEvents
    {
        string AccountId { get; set; }
        bool ShowDeleted { get; set; }
        int Page { get; set; }
        int PageSize { get; set; }
        DateTime Since { get; set; }
        string EventType { get; set; }
    }
}