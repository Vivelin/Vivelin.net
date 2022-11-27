using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Vivelin.Web.Home;

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public static SnakeCaseNamingPolicy SnakeCase { get; } = new();

    /// <inheritdoc/>
    public override string ConvertName(string name)
    {
        return string.Concat(name.Select((x, i) => i > 0 && char.IsUpper(x) 
                ? "_" + x.ToString() 
                : x.ToString()))
            .ToLower();

    }
}
