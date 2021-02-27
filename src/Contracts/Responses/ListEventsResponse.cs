using System.Collections.Generic;

namespace AlbedoTeam.Sdk.EventStore.Contracts.Responses
{
    public interface ListEventsResponse
    {
        int Page { get; set; }

        int PageSize { get; set; }

        int RecordsInPage { get; set; }

        int TotalPages { get; set; }

        string FilterBy { get; set; }

        List<EventResponse> Items { get; set; }
    }
}