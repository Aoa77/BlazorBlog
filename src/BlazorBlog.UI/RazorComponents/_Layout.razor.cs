using BlazorBlog.JS;
using BlazorBlog.UI.Common.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BlazorBlog.UI.RazorComponents;


public abstract class _LayoutBase : LayoutComponentBase, IJSInterop
{
    [Inject]
    public required IJsService JsService { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }
        await JsService.Startup();
    }
}
