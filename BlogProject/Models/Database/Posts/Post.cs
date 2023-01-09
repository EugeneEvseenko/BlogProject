using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlogProject.Models.Database.Users;

namespace BlogProject.Models.Database.Posts;

[Table("tbl_posts")]
public class Post
{
    public Guid Id { get; set; }
    [Required]
    public User User { get; set; }
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    [Required]
    public string Body { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime? EditTime { get; set; }
    public List<Tag> Tags { get; set; }
    public List<Comment> Comments { get; set; }
    
}