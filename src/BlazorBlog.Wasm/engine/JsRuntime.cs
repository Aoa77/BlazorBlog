using Microsoft.JSInterop;

namespace BlazorBlog.Wasm.engine;

public static class JsRuntime
{
    private const string bbjs = nameof(bbjs);

    public static async Task Startup(this IJSRuntime js)
    {
        await js.InvokeVoidAsync($@"{bbjs}.startup");
    }
    public static async Task<int> GetViewWidth(this IJSRuntime js)
    {
        return await js.InvokeAsync<int>($@"{bbjs}.getViewWidth");
    }
}
