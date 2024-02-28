using Microsoft.AspNetCore.Components;

namespace BlazorBlog.UI.Interfaces;

public interface INavigation
{
    NavigationManager NavigationManager { get; }
}
