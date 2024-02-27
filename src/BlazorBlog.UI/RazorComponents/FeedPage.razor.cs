using BlazorBlog.UI.BaseClasses;

namespace BlazorBlog.UI.RazorComponents;

public abstract class FeedPageBase : BlogBase
{
    protected IEnumerable<string> PostIds { get; private set; }
        = Enumerable.Empty<string>();

    protected override void OnInitialized()
    {
        PostIds = DataService.BlogPosts.Keys;
    }

    private int _index = 0;
    protected string StripeColor()
    {
        string color = ++_index % 2 == 0 ? "StripedEven" : "StripedOdd";
        return $@"background-color: var(--{color}_backColor)";
    }
}
