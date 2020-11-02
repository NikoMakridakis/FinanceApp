namespace Web.Models
{
    public class UserDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string AccessToken { get; set; }
        public bool IsLockedOut { get; set; }
        public int LockoutSeconds { get; set; }
    }
}
