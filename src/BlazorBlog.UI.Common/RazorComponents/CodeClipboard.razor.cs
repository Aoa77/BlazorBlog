using BlazorBlog.UI.Common.BaseClasses;
using Microsoft.AspNetCore.Components;

namespace BlazorBlog.UI.Common.RazorComponents;


public abstract class CodeClipboardBase : UICommonBase
{

    [Parameter]
    public string Text { get; set; } = "";
}
