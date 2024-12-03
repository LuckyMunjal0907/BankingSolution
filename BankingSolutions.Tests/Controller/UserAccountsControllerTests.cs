using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSolution.Controllers;
using BankingSolution.Repository;
using BankingSolution.Services.Interfaces;
using BankingSolution.Services;
using Microsoft.Extensions.Options;
using BankingSolution.Models;
using BankingSolution.DTO;
using BankingSolution.Utils;

namespace BankingSolutions.Tests.Controller
{
    public class UserAccountsControllerTests
    {
        private readonly IUserAccountService _userAccountService;
        private readonly UserAccountsController _userAccountsController;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly AccountConfiguration accountConfig;
        public UserAccountsControllerTests()
        {
            accountConfig = new AccountConfiguration()
            {
                MaxDepositLimit = 10000,
                MinBalance = 100,
                WithdrawLimit = 0.90
            };

            IOptions<AccountConfiguration> appSettingsOptions = Options.Create(accountConfig);
            _userAccountRepository = new UserAccountRepository(appSettingsOptions);
            _userAccountService = new UserAccountService(_userAccountRepository);
            _userAccountsController = new UserAccountsController(_userAccountService);

        }

        [Fact]
        public async void GetUserAccountsByAccountNumber_Success()
        {
            int accountNumber = 10000001;
           var response = await _userAccountsController.GetUserAccountByAccountNumber(accountNumber);
            Assert.True(response.IsSuccess);
            Assert.Equal(accountNumber, ((UserAccountDTO)response.Result).AccountNumber);
        }

        [Fact]
        public async void GetUserAccountsByAccountNumber_Fail()
        {
            int accountNumber = 78788777;
            var response = await _userAccountsController.GetUserAccountByAccountNumber(accountNumber);
            Assert.False(response.IsSuccess);
            Assert.NotEqual(accountNumber, ((UserAccountDTO)response.Result).AccountNumber);
        }

        [Fact]
        public async void GetUserAccountsByUserId_Success()
        {
            int userId = 1;
            var response = await _userAccountsController.GetUserAccountsByuser(userId);
            Assert.True(response.IsSuccess);
            Assert.True(((List<UserAccountDTO>)response.Result).Count() > 0);
        }

        [Fact]
        public async void GetUserAccountsByUserId_Fail()
        {
            int userId = 999999;
            var response = await _userAccountsController.GetUserAccountsByuser(userId);
            Assert.False(response.IsSuccess);
            Assert.False(((List<UserAccountDTO>)response.Result).Count() > 0);
        }

        [Fact]
        public async void CheckAccountBalance_Success()
        {
            int accountNumber = 10000003;
            var response = await _userAccountsController.CheckAccountBalance(accountNumber);
            Assert.True(response.IsSuccess);
            Assert.Equal(300, (double)response.Result);
        }

        [Fact]
        public async void CheckAccountBalance_Fail()
        {
            int accountNumber = 99999999;
            var response = await _userAccountsController.CheckAccountBalance(accountNumber);
            Assert.False(response.IsSuccess);
            Assert.Equal( 0.0, (double)response.Result);            
        }

        [Fact]
        public async void DeleteAllAccountsForUser_Success()
        {
            int userId = 3;
            var response = await _userAccountsController.DeleteAllAccountsForUser(userId);
            Assert.True(response.IsSuccess);
            Assert.Equal(string.Format(Constants.DEL_USER_ALLAC_MSG_SUCCESS, userId), response.StatusMessage);
        }

        [Fact]
        public async void DeleteAllAccountsForUser_Fail()
        {
            int userId = 99999;
            var response = await _userAccountsController.DeleteAllAccountsForUser(userId);
            Assert.False(response.IsSuccess);
            Assert.Equal(string.Format(Constants.DEL_USER_ALLAC_MSG_FAILURE, userId), response.StatusMessage);
        }

        [Fact]
        public async void DeleteAccountsByAccountNumber_Success()
        {
            int accountNumber = 10000000;
            var response = await _userAccountsController.DeleteAccountByAccountNumber(accountNumber);
            Assert.True(response.IsSuccess);
            Assert.Equal(string.Format(Constants.DEL_USER_AC_MSG_SUCCESS, accountNumber), response.StatusMessage);
        }

        [Fact]
        public async void DeleteAccountsByAccountNumber_Fail()
        {
            int accountNumber = 9999999;
            var response = await _userAccountsController.DeleteAccountByAccountNumber(accountNumber);
            Assert.False(response.IsSuccess);
            Assert.Equal(string.Format(Constants.DEL_USER_AC_MSG_FAILURE, accountNumber), response.StatusMessage);
        }

        [Fact]
        public async void DepositFunds_Success()
        {
            int accountNumber = 10000006;
            double amount = 1000;
            var response = await _userAccountsController.DepositFunds(accountNumber, amount);
            Assert.True(response.IsSuccess);
            Assert.Equal(1300, ((UserAccountDTO)response.Result).Balance);
        }

        [Fact]
        public async void DepositFunds_NotFound_Fail()
        {
            int accountNumber = 9999999;
            double amount = 1000;
            var response = await _userAccountsController.DepositFunds(accountNumber, amount);
            Assert.False(response.IsSuccess);
            Assert.Equal(response.StatusMessage, string.Format(Constants.ACC_NOT_EXISTS, accountNumber));
            Assert.Equal(0, ((UserAccountDTO)response.Result).Balance);
        }

        [Fact]
        public async void DepositFunds_DepositLimit_Fail()
        {
            int accountNumber = 9999999;
            double amount = 11000;
            var response = await _userAccountsController.DepositFunds(accountNumber, amount);
            Assert.False(response.IsSuccess);
            Assert.Equal(response.StatusMessage, string.Format(Constants.ACC_DEPOSIT_VALIDATION, accountConfig.MaxDepositLimit));
            Assert.Equal(0, ((UserAccountDTO)response.Result).Balance);
        }

        [Fact]
        public async void WithDrawFunds_Success()
        {
            int accountNumber = 10000007;
            double amount = 1000;
            var response = await _userAccountsController.WithdrawFunds(accountNumber, amount);
            Assert.True(response.IsSuccess);
            Assert.Equal(39000,((UserAccountDTO)response.Result).Balance);
        }

        [Fact]
        public async void WithdrawFunds_NotFound_Fail()
        {
            int accountNumber = 9999999;
            double amount = 1000;
            var response = await _userAccountsController.WithdrawFunds(accountNumber, amount);
            Assert.False(response.IsSuccess);
            Assert.Equal(response.StatusMessage, string.Format(Constants.ACC_NOT_EXISTS, accountNumber));
            Assert.Equal(0, ((UserAccountDTO)response.Result).Balance);
        }

        [Fact]
        public async void WithdrawFunds_InSufficientFunds_Fail()
        {
            int accountNumber = 10000002;
            double amount = 5000;
            var response = await _userAccountsController.WithdrawFunds(accountNumber, amount);
            Assert.False(response.IsSuccess);
            Assert.Equal(response.StatusMessage, string.Format(Constants.INSUFF_BALANCE_AC, accountNumber));
            Assert.Equal(3000, ((UserAccountDTO)response.Result).Balance);
        }

        [Fact]
        public async void WithdrawFunds_WithdrawLimit_Fail()
        {
            int accountNumber = 10000002;
            double amount = 2900;
            var response = await _userAccountsController.WithdrawFunds(accountNumber, amount);
            Assert.False(response.IsSuccess);
            Assert.Equal(response.StatusMessage, string.Format(Constants.WITHDRAW_MAX_LIMIT, accountConfig.WithdrawLimit*100));
            Assert.Equal(3000, ((UserAccountDTO)response.Result).Balance);
        }

        [Fact]
        public async void WithdrawFunds_MinBalance_Fail()
        {
            int accountNumber = 10000003;
            double amount = 210;
            var response = await _userAccountsController.WithdrawFunds(accountNumber, amount);
            Assert.False(response.IsSuccess);
            Assert.Equal(response.StatusMessage, string.Format(Constants.WITHDRAW_MIN_BALANCE, amount, accountNumber, accountConfig.MinBalance));
            Assert.Equal(300, ((UserAccountDTO)response.Result).Balance);
        }

        [Fact]
        public async void CreateUserAccount_Success()
        {
            var account = new UserAccountDTO
            {
                UserId = 1,
                AccountType = AccountTypes.Savings,
                Balance = 1000
            };

            var response = await _userAccountsController.CreateUserAccount(account);
            Assert.True(response.IsSuccess);
            Assert.True(((UserAccountDTO)response.Result).AccountNumber > 0);
        }

        [Fact]
        public async void CreateUserAccount_Fail()
        {
            var account = new UserAccountDTO
            {
                UserId = 1000000,
                AccountType = AccountTypes.Savings,
                Balance = 1000
            };

            var response = await _userAccountsController.CreateUserAccount(account);
            Assert.False(response.IsSuccess);
            Assert.Equal(0,((UserAccountDTO)response.Result).AccountNumber);
        }


    }
}
