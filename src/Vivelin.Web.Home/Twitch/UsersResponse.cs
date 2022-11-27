using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Vivelin.Web.Home.Twitch;

public class UsersResponse
{
    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public List<UserInfo> Data { get; set; } = new();
}
