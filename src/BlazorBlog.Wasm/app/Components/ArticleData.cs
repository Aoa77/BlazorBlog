using Microsoft.AspNetCore.Components;
namespace BlazorBlog.Wasm.app.Components;

public abstract class ArticleData : ComponentBase
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


    private int _bullet = 0;
    public int FirstBullet()
    {
        _bullet = 0;
        return NextBullet();
    }
    public int NextBullet()
    {
        return ++_bullet;
    }

}