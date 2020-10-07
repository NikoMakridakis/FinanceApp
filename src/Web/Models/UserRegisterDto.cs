namespace Web.Models
{
    public class UserRegisterDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public decimal MonthlyIncome { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
