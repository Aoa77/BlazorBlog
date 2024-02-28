using BlazorBlog.UI.Classes;
using Microsoft.AspNetCore.Components;

namespace BlazorBlog.UI.Components;


public abstract class CodeClipboardBase : UIBase
{

    [Parameter]
    public string Text { get; set; } = "";
}
