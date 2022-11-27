using System.Text.RegularExpressions;

namespace Vivelin.Web.Home;

internal class SlugParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        if (value == null || value.ToString() == null)
            return null;

        return Regex.Replace(value.ToString()!, "([a-z])([A-Z])", "$1-$2")
                    .ToLowerInvariant();
    }
}
