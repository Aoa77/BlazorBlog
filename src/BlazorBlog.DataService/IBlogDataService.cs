namespace BlazorBlog.DataService;

public interface IBlogDataService
{
    IReadOnlyDictionary<string, BlogPost> BlogPosts { get; }
}
