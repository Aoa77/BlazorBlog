using BlazorBlog.DataService;

namespace BlazorBlog.Pub;

public sealed class PubDataService : BlogDataService
{
    public PubDataService()
    {
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
            Title = title
        });
    }
}


