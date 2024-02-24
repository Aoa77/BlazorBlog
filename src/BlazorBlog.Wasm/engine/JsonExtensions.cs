using System.Text.Json;

namespace BlazorBlog.Wasm.engine;

public static class JsonExtensions
{
    public static string ToJson(this object src)
    {
        return JsonSerializer.Serialize(src);
    }
}