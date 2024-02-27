using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace BlazorBlog.Data;

public sealed class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> options) : base(options)
    {
    }

    public DbSet<BlogPost> BlogPosts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }


}