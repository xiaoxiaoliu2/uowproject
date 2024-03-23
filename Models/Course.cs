namespace uowpublic.Models;

public class Course
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Dept { get; set; }

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }
}