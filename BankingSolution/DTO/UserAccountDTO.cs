using BankingSolution.Utils;

namespace BankingSolution.DTO
{
    public class UserAccountDTO
    {
        public int AccountNumber { get; set; }
        public int UserId { get; set; }

        public AccountTypes AccountType { get; set; }
        public double Balance { get; set; }
        public bool IsActive { get; set; }
    }
}
