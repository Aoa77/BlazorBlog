using BlazorBlog.DataService;
using BlazorBlog.UI.Classes;
using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;

namespace BlazorBlog.Web.Layout.Classes;

public abstract class BlogBase : UIBase
{
    [Inject, JsonIgnore]
    public required IBlogDataService DataService { get; set; }
}
