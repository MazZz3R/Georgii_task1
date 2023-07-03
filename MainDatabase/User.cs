namespace MainDatabase
{
    public class UserBase
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public required string Email { get; set; }
    }

    public class UserCreate : UserBase
    {
        public required string Password { get; set; }
    }

    public class UserView : UserBase
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public Company? Company { get; set; }

        public List<User> followers { get; set; } = new List<User>();
        public List<User> following { get; set; } = new List<User>();
    }

    public class User : UserView
    {
        public required string Password { get; set; }
    }
}
