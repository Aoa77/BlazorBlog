namespace BlazorBlog.DataService;

public class BlogDataService : IBlogDataService
{
    protected readonly Dictionary<string, BlogPost> _blogPosts = new();
    public IReadOnlyDictionary<string, BlogPost> BlogPosts => _blogPosts;
}