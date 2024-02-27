using BlazorBlog.UI.Common.BaseClasses;
using BlazorBlog.UI.Common.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BlazorBlog.UI.Common.RazorComponents;


public abstract class EmbeddedMediaBase : UICommonBase, IFlex
{
    protected double DisplayWidth { get; private set; }
    protected double DisplayHeight { get; private set; }
    protected bool IsVideo { get; private set; } = false;
    protected string LinkStyle { get; private set; } = "";
    protected double LinkWidth { get; private set; }
    protected double LinkHeight { get; private set; }
    protected string MediaSource { get; private set; } = "";

    [Parameter] public required string MediaFile { get; set; } = "";
    [Parameter] public string? MediaPath { get; set; } = null;
    [Parameter] public string? MediaType { get; set; } = null;
    [Parameter] public double Width { get; set; } = 25;
    [Parameter] public double Height { get; set; } = 25;
    [Parameter] public double Scale { get; set; } = 1.0;
    [Parameter] public double? AltFlexScale { get; set; }
    [Parameter] public double? MinimumFlexWidth { get; set; }

    protected override void OnInitialized()
    {
        if (MediaPath is null)
        {
            throw new ArgumentNullException(nameof(MediaPath));
        }

        MediaSource = $@"/{MediaPath.Trim('/')}/{MediaFile.Trim('/')}";
        IsVideo = MediaFile.EndsWith(".mp4");
        MediaType = IsVideo ? "media-video" : "media-image";
        CssClass = $"{MediaType} {CssClass}";
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
            LinkWidth = DisplayWidth;
            LinkHeight = DisplayHeight;

            var borderRem = await JsService.GetRootVariable("--Media_borderWidth");
            var borderPx = 2 * await JsService.ConvertRemToPixels(borderRem);

            LinkWidth += borderPx;
            LinkHeight += borderPx;

            LinkStyle = $@"width: {LinkWidth}px; height: {LinkHeight}px;";
        }

        StateHasChanged();
    }
}

