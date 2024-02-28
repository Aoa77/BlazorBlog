using BlazorBlog.JS;
using BlazorBlog.UI.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;

namespace BlazorBlog.Web.Layout.Components;


public abstract class _LayoutBase : LayoutComponentBase, IJSInterop, INavigation
{
    [Inject, JsonIgnore]
    public IJsService JsService { get; set; } = null!;

    [Inject, JsonIgnore]
    public NavigationManager NavigationManager { get; set; } = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }
        await JsService.Startup();
    }
}
