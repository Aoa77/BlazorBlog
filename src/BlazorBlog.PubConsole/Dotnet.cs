using System.Diagnostics;

namespace BlazorBlog.PubConsole;

public static class Dotnet
{
    private const int DelayStart = 1000;
    public static async Task Execute(string dir, string[] args, int delayStart = DelayStart)
    {
        Console.WriteLine();
        Console.WriteLine($@"- executing ""dotnet {string.Join(' ', args)}"".");
        await Task.Delay(delayStart);
        var p_info = new ProcessStartInfo
        {
            UseShellExecute = true,
            CreateNoWindow = false,
            WindowStyle = ProcessWindowStyle.Normal,
            FileName = @"dotnet",
            Arguments = string.Join(' ', args),
            WorkingDirectory = dir
        };
        Process.Start(p_info);
    }
}

