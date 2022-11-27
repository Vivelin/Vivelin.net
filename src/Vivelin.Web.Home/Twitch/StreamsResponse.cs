using System.Diagnostics;

namespace Vivelin.Web.Home.Twitch;

public class StreamsResponse
{
    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public List<LiveStream> Data { get; set; } = new();

    public PaginationInfo? Pagination { get; set; }
}
