using BankingSolution.DTO;
using BankingSolution.Models;
using BankingSolution.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService )
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("create-user")]
        public async Task<ResponseModel> CreateUser(UserDTO user)
        {
            try
            {
                var response = await _userService.CreateUser(user);

                if (response.UserId <= 0)
                    return new ResponseModel { IsSuccess = false, StatusMessage = "", Result = response };
                else
                    return new ResponseModel { IsSuccess = true, StatusMessage = "", Result = response };
            }
            catch (Exception ex)
            {
                return new ResponseModel { IsSuccess = false, StatusMessage = "There is some Exception", Result = ex.Message.ToString() };
            }
        }

        [HttpPut]
        [Route("update-user")]
        public async Task<ResponseModel> UpdateUser(UserDTO user)
        {
            try
            {
                var response = await _userService.UpdateUser(user);
                return new ResponseModel { IsSuccess = true, StatusMessage = "", Result = response };
            }
            catch (Exception ex)
            {
                return new ResponseModel { IsSuccess = false, StatusMessage = "There is some Exception", Result = ex.Message.ToString() };
            }
        }

        [HttpDelete]
        [Route("delete-user/{id}")]
        public async Task<ResponseModel> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
           return new ResponseModel { IsSuccess = response.Item1, StatusMessage = response.Item2 };
        }

        [HttpGet]
        [Route("getbyid/{id}")]
        public async Task<ResponseModel> GetUserById(int id)
        {
            var response = await _userService.GetUserById(id);
            if (response.UserId <= 0)
                return new ResponseModel { IsSuccess = false, StatusMessage = "", Result = response };
            else
                return new ResponseModel { IsSuccess = true, StatusMessage = "", Result = response };
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ResponseModel> GetAllUsers()
        {
            var response = await _userService.GetAllUsers();
            if (response.Count() == 0)
                return new ResponseModel { IsSuccess = false, StatusMessage = "", Result = response };
            else
                return new ResponseModel { IsSuccess = true, StatusMessage = "", Result = response };
        }

    }
}
