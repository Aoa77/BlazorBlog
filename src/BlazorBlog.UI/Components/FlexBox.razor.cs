using BlazorBlog.UI.Classes;
using BlazorBlog.UI.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BlazorBlog.UI.Components;


public abstract class FlexBoxBase : UIBase, IFlex
{
    [Parameter]
    public double? MinimumFlexWidth { get; set; }

    public string DisplayStyle { get; private set; } = "flex";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }
        if (!MinimumFlexWidth.HasValue)
        {
            return;
        }
        var viewWidth = await JsService.GetViewWidth();
        if (viewWidth < MinimumFlexWidth.Value)
        {
            DisplayStyle = "block";
            StateHasChanged();
        }
    }

}
