namespace uowpublic.Models;

public class Property
{
    public int Id { get; set; }

    public int Publisher_Id { get; set; }

    public string? Address { get; set; }

    public required string Type { get; set; }

    public decimal Rent { get; set; }

    public string? Facilitity { get; set; }

    public string? Description { get; set; }

    public string? Attribute { get; set; }

    public decimal Longitude { get; set; }
    
    public decimal Latitude { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public bool IsDeleted { get; set; }
}

public class PropertyPhoto
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public string? Url { get; set; }

    public bool IsDeleted { get; set; }
}