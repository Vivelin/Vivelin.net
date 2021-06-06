using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Vivelin.Web.Home.Twitch
{
    public class StreamsResponse
    {
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public List<LiveStream> Data { get; set; }

        public PaginationInfo Pagination { get; set; }
    }
}
