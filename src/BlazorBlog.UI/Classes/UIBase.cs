using BlazorBlog.JS;
using BlazorBlog.UI.Interfaces;
using CurrieTechnologies.Razor.Clipboard;
using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;

namespace BlazorBlog.UI.Classes;

public abstract class UIBase : ComponentBase, IJSInterop, INavigation
{
    [Parameter, JsonIgnore]
    public RenderFragment? ChildContent { get; set; }

    [Inject, JsonIgnore]
    public ClipboardService ClipboardService { get; set; } = null!;

    [Parameter, JsonIgnore]
    public string? CssClass { get; set; }

    [Inject, JsonIgnore]
    public IJsService JsService { get; set; } = null!;

    [Inject, JsonIgnore]
    public NavigationManager NavigationManager { get; set; } = null!;
}
