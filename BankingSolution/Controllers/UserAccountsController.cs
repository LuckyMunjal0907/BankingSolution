using System.Diagnostics.Eventing.Reader;
using BankingSolution.DTO;
using BankingSolution.Models;
using BankingSolution.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.Controllers
{
    [Route("api/useraccounts")]
    [ApiController]
    public class UserAccountsController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;
        public UserAccountsController(IUserAccountService userAccountService) { 
          _userAccountService = userAccountService;
        }

        [HttpGet]
        [Route("getbyaccountnumber/{accountNumber}")]
        public async Task<ResponseModel> GetUserAccountByAccountNumber(int accountNumber)
        {
            var response = await _userAccountService.GetUserAccountByAccountNumber(accountNumber);
            if(response.AccountNumber > 0) 
                return new ResponseModel { IsSuccess = true, StatusMessage = "", Result = response } ;
            else
                return new ResponseModel { IsSuccess = false, StatusMessage = "", Result = response };

        }

        [HttpGet]
        [Route("getbyuser/{userid}")]
        public async Task<ResponseModel> GetUserAccountsByuser(int userid)
        {
            var response =(await _userAccountService.GetUserAccountsByUserId(userid)).ToList();   
            if(response.Count > 0)
                return new ResponseModel { IsSuccess = true, StatusMessage = "", Result = response };
            else
                return new ResponseModel { IsSuccess = false, StatusMessage = "", Result = response };

        }

        [HttpGet]
        [Route("checkbalance/{accountNumber}")]
        public async Task<ResponseModel> CheckAccountBalance(int accountNumber)
        {
           var response = await _userAccountService.CheckAccountBalance(accountNumber);
            return new ResponseModel { IsSuccess = response.Item1 > 0 ? true: false, StatusMessage = response.Item2, Result = response.Item1};
        }

        [HttpPost]
        [Route("create-account")]
        public async Task<ResponseModel> CreateUserAccount(UserAccountDTO userAccount)
        {
            var response = await _userAccountService.CreateUserAccount(userAccount);
            if(response.AccountNumber > 0)
                return new ResponseModel { IsSuccess = true, StatusMessage = "", Result = response };
            else
                return new ResponseModel { IsSuccess = false, StatusMessage = "", Result = response };
        }

        [HttpPatch]
        [Route("delete-all")]
        public async Task<ResponseModel> DeleteAllAccountsForUser(int userId)
        {
            var response = await _userAccountService.DeleteAllUserAccounts(userId);
            return new ResponseModel { IsSuccess = response.Item1, StatusMessage = response.Item2 };
        }

        [HttpPatch]
        [Route("delete-by-accountnumber")]
        public async Task<ResponseModel> DeleteAccountByAccountNumber(int accountNumber)
        {
            var response = await _userAccountService.DeleteUserAccountByAccountNumber(accountNumber);
            return new ResponseModel { IsSuccess = response.Item1, StatusMessage = response.Item2 };
        }

        [HttpPost]
        [Route("deposit")]
        public async Task<ResponseModel> DepositFunds(int accountNumber, double amount)
        {
            var response = await _userAccountService.DepositFunds(accountNumber, amount);
            return new ResponseModel { IsSuccess = response.Item1, StatusMessage = response.Item2 , Result = response.Item3 };
        }

        [HttpPost]
        [Route("withdraw")]
        public async Task<ResponseModel> WithdrawFunds(int accountNumber, double amount)
        {
            var response = await _userAccountService.WithdrawFunds(accountNumber, amount);
            return new ResponseModel { IsSuccess = response.Item1, StatusMessage = response.Item2, Result = response.Item3 };
        }

    }
}
