using BankingSolution.DTO;
using BankingSolution.Models;

namespace BankingSolution.Services.Interfaces
{
    public interface IUserAccountService
    {
        Task<IEnumerable<UserAccountDTO>> GetUserAccountsByUserId(int userId);
        Task<UserAccountDTO> GetUserAccountByAccountNumber(int accountNumber);
        Task<Tuple<bool, string>> DeleteUserAccountByAccountNumber(int accountNumber);
        Task<Tuple<bool, string>> DeleteAllUserAccounts(int userId);
        Task<Tuple<bool, string, UserAccountDTO>> DepositFunds(int accountNumber, double amount);
        Task<Tuple<bool, string, UserAccountDTO>> WithdrawFunds(int accountNumber, double amount);
        Task<Tuple<double, string>> CheckAccountBalance(int accountNumber);
        Task<UserAccountDTO> CreateUserAccount(UserAccountDTO account);
    }
}
