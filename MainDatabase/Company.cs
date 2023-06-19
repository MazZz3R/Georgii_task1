namespace MainDatabase
{
    public class Company
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}