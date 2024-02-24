using System.Reflection;
namespace BlazorBlog.Wasm.engine;

public static class BlogManager
{
    public static IReadOnlyDictionary<string, BlogArticle>
        Articles => _articles.Value;

    private static Lazy<IReadOnlyDictionary<string, BlogArticle>>
    _articles = new(() =>
    {
        var articles = new Dictionary<string, BlogArticle>();

        var types = Assembly.GetExecutingAssembly().GetTypes()
        .Where(t => t.IsSubclassOf(typeof(BlogArticle)));

        foreach (var type in types.OrderByDescending(x => x.Name))
        {
            var article = (BlogArticle)
                Activator.CreateInstance(type)!;

            article.Setup();
            articles.Add(article.Key, article);
        }
        return articles;
    });
}
