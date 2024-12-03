using BankingSolution.DTO;
using BankingSolution.Helper;
using BankingSolution.Repository;
using BankingSolution.Services.Interfaces;

namespace BankingSolution.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository;
        public UserAccountService(IUserAccountRepository userAccountRepository) {
            _userAccountRepository = userAccountRepository;
        }
        public async Task<Tuple<double, string>> CheckAccountBalance(int accountNumber)
        {
            
            return await _userAccountRepository.CheckAccountBalance(accountNumber);
        }

        public async Task<UserAccountDTO> CreateUserAccount(UserAccountDTO account)
        {
            return UserAccountModelMapper.ModelToDTO( await _userAccountRepository.CreateUserAccount(UserAccountModelMapper.DTOToModel(account)));
        }

        public async Task<Tuple<bool, string>> DeleteAllUserAccounts(int userId)
        {
            return await _userAccountRepository.DeleteAllUserAccountsByUserId(userId); ;
        
        }

        public async Task<Tuple<bool, string>> DeleteUserAccountByAccountNumber(int accountNumber)
        {
            return await  _userAccountRepository.DeleteUserAccountByAccountNumber(accountNumber); 
        }

        public async Task<Tuple<bool, string, UserAccountDTO>> DepositFunds(int accountNumber, double amount)
        {
            var resp = await _userAccountRepository.DepositFunds(accountNumber, amount);
            return  Tuple.Create(resp.Item1, resp.Item2, UserAccountModelMapper.ModelToDTO(resp.Item3));
        }

        public async Task<UserAccountDTO> GetUserAccountByAccountNumber(int accountNumber)
        {
            return UserAccountModelMapper.ModelToDTO(await  _userAccountRepository.GetUserAccountByAccountNumber(accountNumber));
        }

        public async Task<IEnumerable<UserAccountDTO>> GetUserAccountsByUserId(int userId)
        {
            var userAccounts = new List<UserAccountDTO>();
            foreach (var item in await _userAccountRepository.GetUserAccountsByUserId(userId))
            {
                userAccounts.Add(UserAccountModelMapper.ModelToDTO(item));
            };

            return userAccounts;
        }

        public async Task<Tuple<bool, string, UserAccountDTO>> WithdrawFunds(int accountNumber, double amount)
        {
            var resp = await _userAccountRepository.WithdrawFunds(accountNumber, amount);
            return Tuple.Create(resp.Item1, resp.Item2, UserAccountModelMapper.ModelToDTO(resp.Item3));
        }

    }
}
