using BlazorBlog.JS;
using BlazorBlog.UI.Classes;
using BlazorBlog.UI.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;

namespace BlazorBlog.UI.Components;


public abstract class EmbeddedMediaBase : UIBase, IFlex
{
    protected double DisplayWidth { get; private set; }
    protected double DisplayHeight { get; private set; }
    protected string LinkStyle { get; private set; } = null!;

    private bool IsVideo => MediaFile.EndsWith(".mp4");
    private string MediaType => IsVideo ? "media-video" : "media-image";

    [Parameter, JsonIgnore]
    public Func<string, string, string> MediaFileMapper { get; set; } = null!;
    
    protected string JsonData { get; private set; } = null!;
    [Parameter] public required string MediaFile { get; set; }
    [Parameter] public double Width { get; set; } = 25;
    [Parameter] public double Height { get; set; } = 25;
    [Parameter] public double Scale { get; set; } = 1.0;
    [Parameter] public double? AltFlexScale { get; set; }
    [Parameter] public double? MinimumFlexWidth { get; set; }

    protected override void OnInitialized()
    {
        CssClass = $"{MediaType} {CssClass}";
        MediaFile = MediaFileMapper(NavigationManager.Uri, MediaFile);
        JsonData = this.ToJson();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        var viewWidth = await JsService.GetViewWidth();
        var scale = Scale;

        if (MinimumFlexWidth.HasValue && AltFlexScale.HasValue)
        {
            if (viewWidth < MinimumFlexWidth.Value)
            {
                scale = AltFlexScale.Value;
            }
        }

        var maxWidth = scale * viewWidth;
        var ratio = maxWidth / Width;

        DisplayWidth = Width * ratio;
        DisplayHeight = Height * ratio;

        if (!IsVideo)
        {
            var linkWidth = DisplayWidth;
            var linkHeight = DisplayHeight;

            var borderRem = await JsService.GetRootVariable("--Media_borderWidth");
            var borderPx = 2 * await JsService.ConvertRemToPixels(borderRem);

            linkWidth += borderPx;
            linkHeight += borderPx;

            LinkStyle = $@"width: {linkWidth}px; height: {linkHeight}px;";
        }

        StateHasChanged();
    }
}

