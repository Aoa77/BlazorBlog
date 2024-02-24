using CliWrap.EventStream;
using CliWrap;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BlazorBlog.DevConsole;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var path = GetCallerFileInfo().DirectoryName!;
        path = path.Replace(nameof(DevConsole), "Wasm");
        await DotnetRun(path, "watch");
    }

    private static FileInfo GetCallerFileInfo([CallerFilePath] string path = "")
    {
        return new(path);
    }

    private static async Task DotnetRun(string path, params string[] args)
    {
        KillAll("dotnet");
        KillAll("chrome");

        var p_info = new ProcessStartInfo
        {
            UseShellExecute = true,
            CreateNoWindow = false,
            WindowStyle = ProcessWindowStyle.Normal,
            FileName = @"dotnet",
            Arguments = "watch",
            WorkingDirectory = path
        };
        Process.Start(p_info);

        return;


        Console.WriteLine($"Executing 'dotnet {string.Join(' ', args)}' in {path}");
        var cmd = Cli.Wrap("dotnet")
                     .WithArguments(args)
                     .WithWorkingDirectory(path);

        await foreach (var cmdEvent in cmd.ListenAsync())
        {
            switch (cmdEvent)
            {
                case StartedCommandEvent started:
                    Console.WriteLine($"Process started; ID: {started.ProcessId}");
                    break;
                case StandardOutputCommandEvent stdOut:
                    Console.WriteLine($"Out> {stdOut.Text}");
                    break;
                case StandardErrorCommandEvent stdErr:
                    Console.WriteLine($"Err> {stdErr.Text}");
                    break;
                case ExitedCommandEvent exited:
                    Console.WriteLine($"Process exited; Code: {exited.ExitCode}");
                    break;
            }
        }
    }

    private static void KillAll(string processName)
    {
        Console.WriteLine($"Killing all {processName} processes");
        foreach (var process in Process.GetProcessesByName(processName))
        {
            process.Kill(true);
        }
    }
}
