using BlazorBlog.UI.Common.BaseClasses;
using BlazorBlog.UI.Common.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BlazorBlog.UI.Common.RazorComponents;


public abstract class FlexBoxBase : UICommonBase, IFlex
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
