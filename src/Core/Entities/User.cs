using System.Collections.Generic;

namespace Core.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public decimal MonthlyIncome { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<BudgetGroup> BudgetGroups { get; set; }

        public User()
        {
        }
        public User(decimal monthlyIncome, string email, string password)
        {
            MonthlyIncome = monthlyIncome;
            Email = email;
            Password = password;
        }
    }
}


