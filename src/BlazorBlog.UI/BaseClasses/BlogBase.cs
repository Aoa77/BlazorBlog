using Microsoft.AspNetCore.Components;
using BlazorBlog.UI.Common.BaseClasses;
using BlazorBlog.DataService;

namespace BlazorBlog.UI.BaseClasses;

public abstract class BlogBase : UICommonBase
{
    [Inject]
    public required IBlogDataService DataService { get; set; }
}
