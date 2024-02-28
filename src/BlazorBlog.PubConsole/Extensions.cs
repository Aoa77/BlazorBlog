using System.Diagnostics;

namespace BlazorBlog.PubConsole;

public static class SystemExtensions
{
    public static void KillOrphans(params string[] processNames)
    {
        foreach (var name in processNames)
        {
            Console.WriteLine($@"- killing orphans named ""{name}"".");
            foreach (var process in Process.GetProcessesByName(name))
            {
                process.Kill(true);
            }
        }
    }

    public static void CopyFolderFiles(this DirectoryInfo sourceDir, DirectoryInfo targetDir)
    {
        foreach (var file in sourceDir.GetFiles())
        {
            file.CopyTo(Path.Combine(targetDir.FullName, file.Name),
                overwrite: false); // target files should not exist yet
        }
    }

    public static void CopyRecursive(this DirectoryInfo source, DirectoryInfo target)
    {
        Console.WriteLine($@"- copying from ""{source.FullName}"" to ""{target.FullName}"".");
        source.CopyFolderFiles(target);
        var sourceDirs = source.GetDirectories("*", SearchOption.AllDirectories);
        foreach (var sourceDir in sourceDirs)
        {
            var targetPath = sourceDir.FullName.Replace(source.FullName, "");
            var targetDir = target.CreateSubdirectory(targetPath.TrimStart('\\'));
            sourceDir.CopyFolderFiles(targetDir);
        }
    }
}

