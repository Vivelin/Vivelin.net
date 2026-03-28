using System.Text.RegularExpressions;

namespace Vivelin.Web.Home;

internal partial class SlugParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        if (value == null)
        {
            return null;
        }

        return CamelCaseRegex().Replace(value.ToString() ?? "", "$1-$2")
                    .ToLowerInvariant();
    }

    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex CamelCaseRegex();
}
