using BlazorBlog.DataService;
using Microsoft.AspNetCore.Components;

namespace BlazorBlog.Pub;

public sealed class PubDataService : BlogDataService
{
    private readonly Dictionary<string, Type> _contentTypes = new();

    public PubDataService()
    {
        var contentTypes = this.GetType().Assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(ComponentBase)));

        foreach (var type in contentTypes)
        {
            if (type.Name != nameof(Post)) // see comments in Post.razor
            {
                continue;
            }
            var nmspace = type.Namespace!.Split('.')[^1];
            if (!nmspace.StartsWith('_'))
            {
                continue;
            }
            var id = nmspace.TrimStart('_').Replace('_', '-');
            _contentTypes.Add(id, type);
        }


        AddBlogPost(2024, 01, 20, "Creating a new Angular 17 app with Visual Studio 2022");
        AddBlogPost(2024, 01, 15, "How to create a GitHub template repository for Visual Studio 2022 projects");
        AddBlogPost(2024, 01, 10, "Visual Studio 2022 project templates for Angular, React, and Vue");
    }

    private void AddBlogPost(int year, int month, int day, string title)
    {
        string id = $"{year}-{month:D2}-{day:D2}";
        _blogPosts.Add(id, new()
        {
            Id = id,
            PostDate = new(year, month, day),
            Title = title,
            DynamicComponentType = _contentTypes[id]
        });
    }
}


