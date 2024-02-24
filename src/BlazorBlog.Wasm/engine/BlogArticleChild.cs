using Microsoft.AspNetCore.Components;
namespace BlazorBlog.Wasm.engine;

public abstract class BlogArticleChild : ParentComponent
{
    [Parameter]
    public required BlogArticle Article { get; set; }
}
