using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;
namespace BlazorBlog.Wasm.engine;

public abstract class BlogArticle : ComponentBase
{
    [Parameter] public required string ArticlePath { get; set; }
    [Parameter] public required string ContentPath { get; set; }
    [Parameter] public required DateTime PostDate { get; set; }
    [Parameter] public required string PostDateFmt { get; set; }
    [Parameter] public required string Key { get; set; }
    [Parameter] public required string Title { get; set; }

    public abstract void Setup();

    protected void InitializeParameters(string key, DateTime postDate, string title)
    {
        if (!int.TryParse(key, out _))
        {
            throw new ArgumentException("Key must be an integer", nameof(key));
        }

        var keyType = GetType();
        if (key != keyType.Name.Split('_')[1])
        {
            throw new ArgumentException("Key must match the object type", nameof(key));
        }

        if (key != keyType.FullName!.Split('.')[^2].Split('_')[1])
        {
            throw new ArgumentException("Key must match the content folder", nameof(key));
        }

        Key = key;
        Title = title;
        ArticlePath = $@"/blog/{Key}";
        ContentPath = $@"/content/{Key}";
        PostDate = postDate;
        PostDateFmt = PostDate.ToString("yyyy-MM-dd");
    }

    private static readonly Dictionary<string, int> _bullets = new();

    private int _bullet = 0;
    public int NextBullet([CallerFilePath] string caller = "")
    {
        if (_bullets.TryGetValue(caller, out var bullet))
        {
            return bullet;
        }
        _bullets.Add(caller, ++_bullet);
        return _bullet;
    }

}