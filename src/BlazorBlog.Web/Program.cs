using BlazorBlog.DataService;
using BlazorBlog.JS;
using BlazorBlog.Pub;
using CurrieTechnologies.Razor.Clipboard;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorBlog.Web;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient
        {
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
        });

        builder.Services.AddScoped<IJsService, JsService>();
        builder.Services.AddClipboard();
        builder.Services.AddScoped<IBlogDataService, PubDataService>();

        await builder.Build().RunAsync();
    }
}
