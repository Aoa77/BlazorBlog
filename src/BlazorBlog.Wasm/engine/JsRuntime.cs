using Microsoft.JSInterop;

namespace BlazorBlog.Wasm.engine;

public static class JsRuntime
{
    private const string bbjs = nameof(bbjs);

    public static async Task Startup(this IJSRuntime js)
    {
        await js.InvokeVoidAsync($@"{bbjs}.startup");
    }

    public static async Task<double> GetRootFontSize(this IJSRuntime js)
    {
        return await js.InvokeAsync<double>($@"{bbjs}.getRootFontSize");
    }

    public static async Task<double> GetViewWidth(this IJSRuntime js)
    {
        return await js.InvokeAsync<double>($@"{bbjs}.getViewWidth");
    }

    public static async Task<string> GetRootVariable(this IJSRuntime js, string varName)
    {
        return await js.InvokeAsync<string>($@"{bbjs}.getRootVariable", varName);
    }

    public static async Task<double> ConvertRemToPixels(this IJSRuntime js, string remValue)
    {
        return await js.InvokeAsync<double>($@"{bbjs}.convertRemToPixels", remValue);
    }
}
