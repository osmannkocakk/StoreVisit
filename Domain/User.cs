namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; } // "Admin" or "Standard"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
