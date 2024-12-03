using BankingSolution.Models;

namespace BankingSolution.Repository
{
    public interface IUserAccountRepository
    {
        Task<IEnumerable<UserAccountModel>> GetUserAccountsByUserId(int userId);
        Task<UserAccountModel> GetUserAccountByAccountNumber(int accountNumber);
        Task<Tuple<bool, string>> DeleteUserAccountByAccountNumber(int accountNumber);
        Task<Tuple<bool, string>> DeleteAllUserAccountsByUserId(int userId);
        Task<Tuple<bool, string, UserAccountModel>> DepositFunds(int accountNumber, double amount);
        Task<Tuple<bool, string, UserAccountModel>> WithdrawFunds(int accountNumber, double amount);
        Task<Tuple<double, string>> CheckAccountBalance(int accountNumber);
        Task<UserAccountModel> CreateUserAccount(UserAccountModel account);

    }
}
