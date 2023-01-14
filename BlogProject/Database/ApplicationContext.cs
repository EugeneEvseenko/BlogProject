using BlogProject.Models.Database.Posts;
using BlogProject.Models.Database.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Database;

public class ApplicationContext : IdentityDbContext<User>
{
    public DbSet<User> Users { get; set; }

    public DbSet<Post> UserPosts { get; set; }
    
    public DbSet<Comment> Comments { get; set; }
    
    public DbSet<Tag> Tags { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
}