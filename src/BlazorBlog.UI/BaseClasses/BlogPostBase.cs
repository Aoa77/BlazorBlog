using BlazorBlog.DataService;
using Microsoft.AspNetCore.Components;

namespace BlazorBlog.UI.BaseClasses;

public abstract class BlogPostBase : BlogBase
{
    [Parameter]
    public required string Id { get; set; }

    protected BlogPost BlogPost { get; private set; } = null!;

    protected override void OnInitialized()
    {
        BlogPost = DataService.BlogPosts[Id];
    }
}
