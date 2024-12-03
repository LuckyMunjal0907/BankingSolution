using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSolution.Controllers;
using BankingSolution.DTO;
using BankingSolution.Models;
using BankingSolution.Repository;
using BankingSolution.Utils;
using Microsoft.Extensions.Options;

namespace BankingSolutions.Tests.Repository
{
    public class UserAccountRepositoryTests
    {
        private readonly AccountConfiguration accountConfig;
        private readonly IUserAccountRepository _userAccountRepository;
        public UserAccountRepositoryTests() {

            accountConfig = new AccountConfiguration()
            {
                MaxDepositLimit = 10000,
                MinBalance = 100,
                WithdrawLimit = 0.90
            };

            IOptions<AccountConfiguration> appSettingsOptions = Options.Create(accountConfig);
            _userAccountRepository = new UserAccountRepository(appSettingsOptions);
        }

        [Fact]
        public async void GetUserAccountsByAccountNumber_Success()
        {
            int accountNumber = 10000001;
            var response = await _userAccountRepository.GetUserAccountByAccountNumber(accountNumber);
            Assert.Equal(accountNumber, response.AccountNumber);

        }

        [Fact]
        public async void GetUserAccountsByAccountNumber_Failure()
        {
            int accountNumber = 78788777;
            var response = await _userAccountRepository.GetUserAccountByAccountNumber(accountNumber);
            Assert.Equal(0, response.AccountNumber);
        }

        [Fact]
        public async void GetUserAccountsByUserId_Success()
        {
            int userId = 1;
            var response = await _userAccountRepository.GetUserAccountsByUserId(userId);
            Assert.True(response.Count() > 0);
        }


        [Fact]
        public async void GetUserAccountsByUserId_Fail()
        {
            int userId = 999;
            var response = await _userAccountRepository.GetUserAccountsByUserId(userId);
            Assert.True(response.Count() == 0);
        }

        [Fact]
        public async void CheckAccountBalance_Success()
        {
            int accountNumber = 10000003;
            var response = await _userAccountRepository.CheckAccountBalance(accountNumber);
            Assert.Equal(300, response.Item1);
        }

        [Fact]
        public async void CheckAccountBalance_Fail()
        {
            int accountNumber = 9999999;
            var response = await _userAccountRepository.CheckAccountBalance(accountNumber);
            Assert.Equal(0.0, response.Item1);
        }

        [Fact]
        public async void DeleteAllAccountsForUser_Success()
        {
            int userId = 2;
            var response = await _userAccountRepository.DeleteAllUserAccountsByUserId(userId);
            Assert.True(response.Item1);
        }

        [Fact]
        public async void DeleteAllAccountsForUser_Fail()
        {
            int userId = 99999;
            var response = await _userAccountRepository.DeleteAllUserAccountsByUserId(userId);
            Assert.False(response.Item1);
        }

        [Fact]
        public async void DeleteAccountsByAccountNumber_Success()
        {
            int accountNumber = 10000010;
            var response = await _userAccountRepository.DeleteUserAccountByAccountNumber(accountNumber);
            Assert.True(response.Item1);
        }

        [Fact]
        public async void DeleteAccountsByAccountNumber_Fail()
        {
            int accountNumber = 99999999;
            var response = await _userAccountRepository.DeleteUserAccountByAccountNumber(accountNumber);
            Assert.False(response.Item1);
        }

        [Fact]
        public async void DepositFunds_Success()
        {
            int accountNumber = 10000004;
            double amount = 1000;
            var response = await _userAccountRepository.DepositFunds(accountNumber, amount);
            Assert.True(response.Item1);
            Assert.Equal(41000, response.Item3.Balance);
        }

        [Fact]
        public async void DepositFunds_NotFound_Fail()
        {
            int accountNumber = 9999999;
            double amount = 1000;
            var response = await _userAccountRepository.DepositFunds(accountNumber, amount);
            Assert.False(response.Item1);
            Assert.Equal(0, response.Item3.Balance);
        }

        [Fact]
        public async void DepositFunds_DepositLimit_Fail()
        {
            int accountNumber = 9999999;
            double amount = 11000;
            var response = await _userAccountRepository.DepositFunds(accountNumber, amount);
            Assert.False(response.Item1);
            Assert.Equal(0, response.Item3.Balance);
        }

        [Fact]
        public async void WithDrawFunds_Success()
        {
            int accountNumber = 10000001;
            double amount = 1000;
            var response = await _userAccountRepository.WithdrawFunds(accountNumber, amount);
            Assert.True(response.Item1);
            Assert.Equal(39000,response.Item3.Balance);
        }

        [Fact]
        public async void WithdrawFunds_NotFound_Fail()
        {
            int accountNumber = 9999999;
            double amount = 1000;
            var response = await _userAccountRepository.WithdrawFunds(accountNumber, amount);
            Assert.False(response.Item1);
            Assert.Equal(0, response.Item3.Balance);
        }

        [Fact]
        public async void WithdrawFunds_InSufficientFunds_Fail()
        {
            int accountNumber = 10000002;
            double amount = 5000;
            var response = await _userAccountRepository.WithdrawFunds(accountNumber, amount);
            Assert.False(response.Item1);
            Assert.Equal(3000, response.Item3.Balance);
        }

        [Fact]
        public async void WithdrawFunds_WithdrawLimit_Fail()
        {
            int accountNumber = 10000002;
            double amount = 2900;
            var response = await _userAccountRepository.WithdrawFunds(accountNumber, amount);
            Assert.False(response.Item1);
            Assert.Equal(3000, response.Item3.Balance);
        }

        [Fact]
        public async void WithdrawFunds_MinBalance_Fail()
        {
            int accountNumber = 10000003;
            double amount = 210;
            var response = await _userAccountRepository.WithdrawFunds(accountNumber, amount);
            Assert.False(response.Item1);
            Assert.Equal(300, response.Item3.Balance);
        }

        [Fact]
        public async void CreateUserAccount_Success()
        {
            var account = new UserAccountModel
            {
                UserId = 1,
                AccountType = AccountTypes.Savings,
                Balance = 1000
            };

            var response = await _userAccountRepository.CreateUserAccount(account);
            Assert.True(response.AccountNumber > 0);        }

        [Fact]
        public async void CreateUserAccount_Fail()
        {
            var account = new UserAccountModel
            {
                UserId = 1000000,
                AccountType = AccountTypes.Savings,
                Balance = 1000
            };

            var response = await _userAccountRepository.CreateUserAccount(account);
            Assert.Equal(0, response.AccountNumber);
        }


    }
}
