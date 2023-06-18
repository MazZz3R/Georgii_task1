public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }

    public required string Password { get; set; }
    public bool Enabled { get; set; }
    public required string Email { get; set; }
    public Company? Company { get; set; }

    public List<User> followers { get; set; } = new List<User>();
    public List<User> following { get; set; } = new List<User>();
}