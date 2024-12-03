using System.Formats.Asn1;
using System.Reflection.Metadata.Ecma335;
using BankingSolution.Data;
using BankingSolution.Models;
using BankingSolution.Utils;
using Microsoft.Extensions.Options;

namespace BankingSolution.Repository
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly AccountConfiguration _accountConfig;
        public UserAccountRepository(IOptions<AccountConfiguration> options) { 

                    _accountConfig = options.Value;
        }
        public async Task<Tuple<bool, string>> DeleteAllUserAccountsByUserId(int userId)
        {

            var accounts = await Task.Run(() =>  DbSets.userAccounts.Where(a => a.UserId == userId && a.IsActive));

            if (accounts.Any()) {
                foreach (var account in accounts)
                {
                    account.IsActive = false;
                }
                return Tuple.Create(true, string.Format(Constants.DEL_USER_ALLAC_MSG_SUCCESS, userId));
            }
            else return  Tuple.Create(false, string.Format(Constants.DEL_USER_ALLAC_MSG_FAILURE, userId));

        }

        public async Task<Tuple<bool, string>> DeleteUserAccountByAccountNumber(int accountNumber)
        { 
                var account = await Task.Run(() => DbSets.userAccounts.FirstOrDefault(x => x.AccountNumber == accountNumber && x.IsActive));
                if (account != null)
                {
                    account.IsActive = false;
                    return Tuple.Create(true, string.Format(Constants.DEL_USER_AC_MSG_SUCCESS, accountNumber));
                }
                else
                    return  Tuple.Create(false, string.Format(Constants.DEL_USER_AC_MSG_FAILURE, accountNumber));
           
        }

        public async Task<Tuple<bool, string, UserAccountModel>> DepositFunds(int accountNumber, double amount)
        {
            if(amount > _accountConfig.MaxDepositLimit)
            {
                return Tuple.Create(false, string.Format(Constants.ACC_DEPOSIT_VALIDATION, _accountConfig.MaxDepositLimit), new UserAccountModel());
            }
            var account = await Task.Run(()=> DbSets.userAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber));
            if (account != null)
            {
                account.Balance += amount;
                return Tuple.Create(true, string.Format(Constants.ACC_DEPOSIT_SUCCESS, amount, accountNumber, account.Balance), account);               
            }
            return Tuple.Create(false, string.Format(Constants.ACC_NOT_EXISTS, accountNumber), new UserAccountModel()  );
        }

        public async Task<Tuple<double, string>> CheckAccountBalance(int accountNumber)
        {
            var account = await Task.Run(() => DbSets.userAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber));
            if (account == null)
            {
                return Tuple.Create(0.0, string.Format(Constants.ACC_NOT_EXISTS, accountNumber));
            }
            return Tuple.Create(account.Balance, string.Format(Constants.ACC_CHK_BALANCE, accountNumber, account.Balance));
        }

        public async Task<UserAccountModel> GetUserAccountByAccountNumber(int accountNumber)
        {
            var account = await Task.Run(() => DbSets.userAccounts.SingleOrDefault(x => x.AccountNumber == accountNumber && x.IsActive));
            if (account == null)
                return new UserAccountModel();
            else
                return account;
        }

        public async Task<IEnumerable<UserAccountModel>> GetUserAccountsByUserId(int userId)
        {
            return await Task.Run(() => DbSets.userAccounts.Where(x => x.UserId == userId && x.IsActive));
        }

        public async Task<Tuple<bool, string, UserAccountModel>> WithdrawFunds(int accountNumber, double amount)
        {
            var account = await Task.Run(() => DbSets.userAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber));
            if (account == null)
            {
                return Tuple.Create(false, string.Format(Constants.ACC_NOT_EXISTS, accountNumber), new UserAccountModel());
            }

            if(amount > account.Balance)
                return Tuple.Create(false, string.Format(Constants.INSUFF_BALANCE_AC, accountNumber), account);
            else if (amount > account.Balance * _accountConfig.WithdrawLimit)
                return Tuple.Create(false, string.Format(Constants.WITHDRAW_MAX_LIMIT, _accountConfig.WithdrawLimit*100), account);
            else if (account.Balance - amount < _accountConfig.MinBalance)
                return Tuple.Create(false, string.Format(Constants.WITHDRAW_MIN_BALANCE, amount, accountNumber, _accountConfig.MinBalance), account);

            account.Balance -= amount;
            return Tuple.Create(true, string.Format(Constants.WITHDRAW_SUCCESS, amount, accountNumber, account.Balance), account);
        }

        public async Task <UserAccountModel> CreateUserAccount(UserAccountModel userAccountModel)
        {
            if (DbSets.users.Any(u => u.UserId == userAccountModel.UserId))
            {
                userAccountModel.AccountNumber = new Random().Next(11000000, 99999999);
                await Task.Run(() => DbSets.userAccounts.Add(userAccountModel));
                return userAccountModel;
            }
            else
                return new UserAccountModel();
            
        }
    }
}
