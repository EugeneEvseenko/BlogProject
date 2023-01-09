using BlogProject.Models.ViewModels.Posts;

public class PostViewModel
{
    public string Author { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Body { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime? EditTime { get; set; }

    public bool WasEdit => EditTime.HasValue; 
    public List<string> Tags { get; set; } 
    public List<CommentViewModel> Comments { get; set; }
}