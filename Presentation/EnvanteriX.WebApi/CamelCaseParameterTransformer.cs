using System.Text.RegularExpressions;

public class CamelCaseParameterTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object value)
    {
        if (value == null) { return null; }

        // İlk harfi küçük yaparak camelCase formatına çevir
        var result = Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();

        // Sonuç çıktı: örneğin "MyAction" -> "myAction"
        return char.ToLowerInvariant(result[0]) + result.Substring(1);
    }
}
