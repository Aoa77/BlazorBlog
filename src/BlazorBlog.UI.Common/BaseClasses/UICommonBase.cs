using BlazorBlog.JS;
using BlazorBlog.UI.Common.Interfaces;
using CurrieTechnologies.Razor.Clipboard;
using Microsoft.AspNetCore.Components;

namespace BlazorBlog.UI.Common.BaseClasses;

public abstract class UICommonBase : ComponentBase, IJSInterop
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Inject]
    public required ClipboardService ClipboardService { get; set; }

    [Parameter]
    public string? CssClass { get; set; } = null;

    [Inject]
    public required IJsService JsService { get; set; }
}
