using Microsoft.AspNetCore.Components;
using BlazorBlog.DataService;

namespace BlazorBlog.Web.Layout.Classes;

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
