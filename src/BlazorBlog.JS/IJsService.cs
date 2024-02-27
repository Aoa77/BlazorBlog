
namespace BlazorBlog.JS;

public interface IJsService
{
    ValueTask<double> ConvertRemToPixels(string remValue);
    ValueTask DisposeAsync();
    ValueTask<double> GetRootFontSize();
    ValueTask<string> GetRootVariable(string varName);
    ValueTask<double> GetViewWidth();
    ValueTask<string> Prompt(string message);
    ValueTask Startup();
}