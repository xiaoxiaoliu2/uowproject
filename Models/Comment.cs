namespace uowpublic.Models;

public class Comment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PostId { get; set; }

    public required string Content { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public bool IsDeleted { get; set; }
}

public enum LikeType
{
    POST,
    COMMENT,
}
public class Like
{
    public int Id { get; set; }
    public int UserId { get; set; }
    
    public int TargetId { get; set; }

    public required LikeType Type { get; set; } 

    public Like() {}

    public bool IsDeleted { get; set; }
}