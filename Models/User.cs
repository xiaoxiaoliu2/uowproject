namespace uowpublic.Models;

public class User
{
    public int Id { get; set; }

    public required string Username { get; set; }

    public required string Password { get; set; }

    public required string Email { get; set; }

    public required string Type { get; set; }
    
    public required string Token { get; set; }

    public bool IsDeleted { get; set; }
}

