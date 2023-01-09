namespace BlogProject.Models.ViewModels.Posts;

public class CommentViewModel
{
    public string Author { get; set; }
    public string Text { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? EditTime { get; set; }
    public bool WasEdit => EditTime.HasValue;
}