using BlazorBlog.JS;
using BlazorBlog.Pub;
using System.Runtime.CompilerServices;

namespace BlazorBlog.PubConsole;

internal sealed class LocalDeployment
{
    private const int ContinueSeconds = 3;
    public string TargetPath => Target.FullName;

    private DirectoryInfo? Caller { get; }
    private DirectoryInfo Source { get; }
    private DirectoryInfo Source_wwwroot { get; }
    private DirectoryInfo Source_content { get; }
    private DirectoryInfo[] Source_resx { get; }

    private DirectoryInfo Target { get; }
    private DirectoryInfo Target_wwwroot { get; }
    private DirectoryInfo Target_content { get; }

    private static (string, string) DefaultConfig([CallerFilePath] string path = "")
    {
        var caller = new FileInfo(path).Directory!;
        var sourceDir = caller.FullName.Replace(nameof(PubConsole), "Pub");
        var targetDir = caller.FullName.Replace(nameof(PubConsole), "Web");
        return (sourceDir, targetDir);
    }

    public LocalDeployment() : this(DefaultConfig())
    {
    }

    private LocalDeployment((string sourceDir, string targetDir) dirs)
        : this(dirs.sourceDir, dirs.targetDir)
    {

    }

    public LocalDeployment(string sourceDir, string targetDir)
    {
        Source = new(sourceDir);
        Source_wwwroot = Source.CreateSubdirectory("wwwroot");
        Source_content = Source.CreateSubdirectory("content");
        Source_resx = Source_content.GetDirectories("resx", SearchOption.AllDirectories);

        Target = new(targetDir);
        Target_wwwroot = Target.CreateSubdirectory("wwwroot");
        Target_content = Target_wwwroot.CreateSubdirectory("resx").CreateSubdirectory("content");
    }

    public void CleanTarget()
    {
        Console.WriteLine();
        Console.WriteLine($@"- cleaning target ""{Target.FullName}"".");
        Target_wwwroot.Delete(true);
        Target_wwwroot.Create();
        Target_content.Create();
    }

    public void CopyContentResx()
    {
        Console.WriteLine();
        foreach (var sourceDir in Source_resx)
        {
            var targetPostDir = Target_content.CreateSubdirectory(sourceDir.Parent!.Name);
            sourceDir.CopyRecursive(targetPostDir);
        }
    }

    public void CopyWwwroot()
    {
        Console.WriteLine();
        Source_wwwroot.CopyRecursive(Target_wwwroot);
    }

    public void KillOrphans()
    {
        Console.WriteLine();
        SystemExtensions.KillOrphans("chrome", "dotnet");
    }

    public void TestDataService()
    {
        var dataService = new PubDataService();
        Console.WriteLine();
        Console.WriteLine($@"- testing data service: {dataService.GetType()}");
        foreach (var post in dataService.BlogPosts.Values)
        {
            Console.WriteLine(post.ToJson(true));
        }
    }

    public void WriteContinueMessage(int seconds = ContinueSeconds)
    {
        Console.WriteLine();
        Console.WriteLine($@"- continuing in {seconds} seconds.");
        while (seconds-- > 0)
        {
            Thread.Sleep(1000);
        }
    }

    public void WriteStartMessage()
    {
        var msg = $@"-- {this.GetType().FullName} --";
        Console.WriteLine(msg);
        Console.WriteLine(new string('=', msg.Length));
    }
}
