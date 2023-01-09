using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlogProject.Models.Database.Users;

namespace BlogProject.Models.Database.Posts;

[Table("tbl_comments")]
public class Comment
{
    public Guid Id { get; set; }
    [Required]
    public User User { get; set; }
    [Required]
    public string Text { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? EditTime { get; set; }
}