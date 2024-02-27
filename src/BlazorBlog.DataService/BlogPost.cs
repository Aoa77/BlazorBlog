namespace BlazorBlog.DataService;

public sealed class BlogPost
{
    public string BlogPath => $@"/blog/{Id}";
    public string ResourcePath => $@"/resx/{Id}";

    public required string Id { get; set; }
    public DateTime PostDate { get; set; }
    public required string Title { get; set; }
}


