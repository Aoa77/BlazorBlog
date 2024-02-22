using BlazorBlog.Wasm.app.Components;
using System.Reflection;
namespace BlazorBlog.Wasm;

public static class BlogManager
{
    public static IReadOnlyDictionary<string, ArticleData>
        Articles => _articles.Value;

    private static Lazy<IReadOnlyDictionary<string, ArticleData>>
    _articles = new(() =>
    {
        var articles = new Dictionary<string, ArticleData>();

        var types = Assembly.GetExecutingAssembly().GetTypes()
        .Where(t => t.IsSubclassOf(typeof(ArticleData)));

        foreach (var type in types.OrderByDescending(x => x.Name))
        {
            var article = (ArticleData)
                Activator.CreateInstance(type)!;

            article.Setup();
            articles.Add(article.Key, article);
        }
        return articles;
    });
}
