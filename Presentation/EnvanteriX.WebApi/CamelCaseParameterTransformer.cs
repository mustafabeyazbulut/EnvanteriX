using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;

public class CamelCaseParameterTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object value)
    {
        if (value == null) return null;

        var result = Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2");

        // Kültürden bağımsız olarak küçük harfe çevir (I -> i)
        result = result.ToLower(CultureInfo.InvariantCulture);

        return result;
    }
}
