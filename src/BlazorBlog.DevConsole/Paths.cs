using System.Runtime.CompilerServices;

namespace BlazorBlog.DevConsole;

internal sealed class Paths
{
    public DirectoryInfo Caller { get; }
    public DirectoryInfo Source { get; }
    public DirectoryInfo Source_wwwroot { get; }
    public DirectoryInfo Source_content { get; }
    public DirectoryInfo[] Source_resx { get; }

    public DirectoryInfo Target { get; }
    public DirectoryInfo Target_wwwroot { get; }
    public DirectoryInfo Target_content { get; }

    public Paths([CallerFilePath] string path = "")
    {
        Caller = new FileInfo(path).Directory!;
        Source = new(Caller.FullName.Replace(nameof(DevConsole), "Pub"));
        Source_wwwroot = Source.CreateSubdirectory("wwwroot");
        Source_content = Source.CreateSubdirectory("content");
        Source_resx = Source_content.GetDirectories("resx", SearchOption.AllDirectories);

        Target = new(Caller.FullName.Replace(nameof(DevConsole), "Web"));
        Target_wwwroot = Target.CreateSubdirectory("wwwroot");
        Target_content = Target_wwwroot.CreateSubdirectory("resx").CreateSubdirectory("content");
    }

    public void CleanTarget()
    {
        Target_wwwroot.Delete(true);
        Target_wwwroot.Create();
        Target_content.Create();
    }
}
