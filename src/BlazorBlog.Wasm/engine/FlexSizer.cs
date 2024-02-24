namespace BlazorBlog.Wasm.engine;

public sealed class FlexSizer
{
    public int MinimumFlexWidth { get; set; } = 768;

    public static class DisplayStyle
    {
        public const string block = nameof(block);
        public const string flex = nameof(flex);
    }

    public static class DeviceWidth
    {
        public const int Mobile = 768;
        public const int Tablet = 992;
        public const int Desktop = 1200;
    }
}
