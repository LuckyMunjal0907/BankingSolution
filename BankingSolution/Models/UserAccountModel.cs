using BankingSolution.Utils;

namespace BankingSolution.Models
{
    public class UserAccountModel
    {
        public int AccountNumber { get; set; }
        public int UserId { get; set; }

        public AccountTypes AccountType { get; set; }
        public double Balance { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
