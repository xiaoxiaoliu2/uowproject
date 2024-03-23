namespace uowpublic.Models;

public class Post
{
    public int Id { get; set; }

    public int UserId  { get; set; }

    public required string Title { get; set; }

    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public bool IsDeleted { get; set; }
}

public class PostPhoto
{
    public int Id { get; set; }

    public int PostId { get; set; }

    public required string Url { get; set; }

    public bool IsDeleted { get; set; }
}

public class PostTag
{
    public int PostId { get; set; }

    public int TagId { get; set; }

    public bool IsDeleted { get; set; }
}

public class Tag
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public bool IsDeleted { get; set; }
}