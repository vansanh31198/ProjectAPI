namespace ProjectAPI.Domain.Entities
{
    public class Users
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? PasswordHash { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
