using System.Text.Json;

namespace BlazorBlog.JS;

public static class JsonExtensions
{
    private static readonly Lazy<JsonSerializerOptions> _options
        = new(() => new JsonSerializerOptions
        {
            WriteIndented = true
        });

    public static string ToJson(this object src, bool format = false)
    {
        return JsonSerializer.Serialize(src, format ? _options.Value : null);
    }
}
