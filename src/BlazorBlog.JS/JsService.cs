using Microsoft.JSInterop;

namespace BlazorBlog.JS;

public sealed class JsService : IAsyncDisposable, IJsService
{

    private readonly Lazy<Task<IJSObjectReference>> _module;

    public JsService(IJSRuntime jsRuntime)
    {
        _module = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/BlazorBlog.JS/BlazorBlog.js").AsTask());
    }

    public async ValueTask<string> Prompt(string message)
    {
        var module = await _module.Value;
        return await module.InvokeAsync<string>("showPrompt", message);
    }

    public async ValueTask DisposeAsync()
    {
        if (_module.IsValueCreated)
        {
            var module = await _module.Value;
            await module.DisposeAsync();
        }
    }



    private const string bbjs = nameof(bbjs);

    public async ValueTask Startup()
    {
        var js = await _module.Value;
        await js.InvokeVoidAsync($@"{bbjs}.startup");
    }

    public async ValueTask<double> GetRootFontSize()
    {
        var js = await _module.Value;
        return await js.InvokeAsync<double>($@"{bbjs}.getRootFontSize");
    }

    public async ValueTask<double> GetViewWidth()
    {
        var js = await _module.Value;
        return await js.InvokeAsync<double>($@"{bbjs}.getViewWidth");
    }

    public async ValueTask<string> GetRootVariable(string varName)
    {
        var js = await _module.Value;
        return await js.InvokeAsync<string>($@"{bbjs}.getRootVariable", varName);
    }

    public async ValueTask<double> ConvertRemToPixels(string remValue)
    {
        var js = await _module.Value;
        return await js.InvokeAsync<double>($@"{bbjs}.convertRemToPixels", remValue);
    }
}
