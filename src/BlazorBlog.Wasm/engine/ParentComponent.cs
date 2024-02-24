using Microsoft.AspNetCore.Components;
namespace BlazorBlog.Wasm.engine;

public abstract class ParentComponent : ComponentBase
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
