using BlazorBlog.UI.BaseClasses;

namespace BlazorBlog.UI.RazorComponents;


public abstract class BlogPageBase : BlogPostBase
{
    protected Type ContentType { get; private set; } = null!;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        ContentType = null;
    }
}
