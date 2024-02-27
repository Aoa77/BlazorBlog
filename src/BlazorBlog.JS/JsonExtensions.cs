using System.Text.Json;

namespace BlazorBlog.JS;

public static class JsonExtensions
{
    public static string ToJson(this object src)
    {
        return JsonSerializer.Serialize(src);
    }
}
