namespace BlazorBlog.PubConsole;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var local = new LocalDeployment();
        local.WriteStartMessage();
        local.KillOrphans();
        local.TestDataService();

        local.CleanTarget();
        local.CopyWwwroot();
        local.CopyContentResx();
        local.WriteContinueMessage();

        await Dotnet.Execute(local.TargetPath, ["watch"]);
    }
}
