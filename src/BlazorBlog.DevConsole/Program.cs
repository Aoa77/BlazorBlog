using System.Diagnostics;
namespace BlazorBlog.DevConsole;

internal static class Program
{
    private readonly static Paths Paths = new();

    private static void Main(string[] args)
    {
        KillOrphans();

        Paths.CleanTarget();
        CopyWwwroot();
        CopyContentResx();

        DotnetRun("watch");
    }

    internal static void KillOrphans()
    {
        KillAll("dotnet");
        KillAll("chrome");

        void KillAll(string processName)
        {
            Console.WriteLine($"Killing all {processName} processes");
            foreach (var process in Process.GetProcessesByName(processName))
            {
                process.Kill(true);
            }
        }
    }
    
    internal static void CopyWwwroot()
    {
        CopyRecursive(Paths.Source_wwwroot, Paths.Target_wwwroot);
    }

    internal static void CopyContentResx()
    {
        foreach (var sourceDir in Paths.Source_resx)
        {
            var targetPostDir = Paths.Target_content.CreateSubdirectory(sourceDir.Parent!.Name);
            CopyRecursive(sourceDir, targetPostDir);
        }
    }

    internal static void DotnetRun(params string[] args)
    {
        var p_info = new ProcessStartInfo
        {
            UseShellExecute = true,
            CreateNoWindow = false,
            WindowStyle = ProcessWindowStyle.Normal,
            FileName = @"dotnet",
            Arguments = "watch",
            WorkingDirectory = Paths.Target.FullName
        };
        Process.Start(p_info);
    }


    
    private static void CopyFolderFiles(DirectoryInfo sourceDir, DirectoryInfo targetDir)
    {
        foreach (var file in sourceDir.GetFiles())
        {
            file.CopyTo(Path.Combine(targetDir.FullName, file.Name),
                overwrite: false); // target files should not exist yet
        }
    }
    private static void CopyRecursive(DirectoryInfo source, DirectoryInfo target)
    {
        CopyFolderFiles(source, target);
        var sourceDirs = source.GetDirectories("*", SearchOption.AllDirectories);
        foreach (var sourceDir in sourceDirs)
        {
            var targetPath = sourceDir.FullName.Replace(source.FullName, "");
            var targetDir = target.CreateSubdirectory(targetPath.TrimStart('\\'));
            CopyFolderFiles(sourceDir, targetDir);
        }
    }
}
