using BlazorBlog.UI.Common.BaseClasses;
using Microsoft.AspNetCore.Components;

namespace BlazorBlog.UI.Common.RazorComponents;


public abstract class StepContainerBase : UICommonBase
{
    protected string LabelCssClass { get; private set; } = "circle-1-digit";
    protected string OddOrEven { get; private set; } = "odd";

    [Parameter]
    public int Number { get; set; }

    [Parameter]
    public RenderFragment? Title { get; set; }

    protected override void OnInitialized()
    {
        LabelCssClass = $"circle-{Number.ToString().Length}-digit";
        OddOrEven = Number % 2 == 0 ? "even" : "odd";
    }
}
