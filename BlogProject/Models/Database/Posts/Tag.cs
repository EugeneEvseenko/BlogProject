using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.Models.Database.Posts;

[Table("tbl_tags")]
public class Tag
{
    public Guid Id { get; set; }
    public string Text { get; set; }
}