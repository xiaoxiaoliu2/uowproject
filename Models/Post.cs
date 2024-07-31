using System.ComponentModel.DataAnnotations.Schema;

namespace uowpublic.Models;

public class Post
{
    public int Id { get; set; }

    [Column("User_Id")]
    public int User_Id  { get; set; }

    public required string Title { get; set; }

    public string? Content { get; set; }

    public DateTime Created_At { get; set; } = DateTime.Now;

    public bool IsDeleted { get; set; }

    public List<Post_Tag_Output>? Tags { get; set; }
    public List<Post_Photo>? Photos { get; set; }
}

public class Post_Photo
{
    public int Id { get; set; }

    [Column("Post_Id")]
    public int Post_Id { get; set; }

    public required string Url { get; set; }

    public bool IsDeleted { get; set; }
}

public class Post_Tag
{
    public int Post_Id { get; set; }

    public int Tag_Id { get; set; }

    public bool IsDeleted { get; set; }
}

public class Post_Tag_Output
{
    public int Post_Id { get; set; }

    public int Tag_Id { get; set; }

    public string? Name { get; set; }

    public bool IsDeleted { get; set; }
}

public class Tag
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public bool IsDeleted { get; set; }
}