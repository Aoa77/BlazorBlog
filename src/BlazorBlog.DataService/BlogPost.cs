using System.Text.Json.Serialization;

namespace BlazorBlog.DataService;

public sealed class BlogPost
{
    public static string ConvertToResourcePath(string url, string path)
    {
        url = url.TrimEnd('/');
        if (!Char.IsDigit(url.Last()))
        {
            throw new InvalidOperationException("The path must end with a BlogPost Id.");
        }
        url = url.Replace("/blog/", "/resx/content/");
        url += $@"/{path.TrimStart('/')}";
        return url;
    }

    public string BlogPath => $@"/blog/{Id}";
    public string ResourcePath => $@"/resx/{Id}";
    public string DisplayDate => PostDate.ToString("yyyy-MM-dd");

    public required string Id { get; set; }
    public DateTime PostDate { get; set; }
    public required string Title { get; set; }

    [JsonIgnore]
    public Type? DynamicComponentType { get; set; }
}


