using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using BankingSolution.Controllers;
using BankingSolution.DTO;
using BankingSolution.Models;
using BankingSolution.Repository;
using BankingSolution.Services;
using BankingSolution.Services.Interfaces;
using BankingSolution.Utils;
using Moq;
using NuGet.Frameworks;

namespace BankingSolutions.Tests.Controller
{
    public class UserControllerTests
    {
        private readonly IUserService _userService;
        private readonly UserController _userController;
        private readonly IUserRepository _userRepository;
        public UserControllerTests() {
            _userRepository = new UserRepository();
            _userService = new UserService(_userRepository);
            _userController = new UserController(_userService);
                
        }

       
        [Fact]
        public async void CreateUser_Success()
        {
            UserDTO user = new UserDTO
            {
                FirstName = "FTest",
                LastName = "LTest",
                Dob = new DateOnly(1993, 12, 8),
                Email = "FTestLTest@gmail.com",
                PhoneNumber = "6467393399",
                Address = "Test street",
                City = "Test City",
                State = "Test State",
                Country = "Test Country",
                PostalCode = "66367",
                IsActive = true
            };

            var response = await _userController.CreateUser(user);
            Assert.True( response.IsSuccess);
        }


        [Fact]
        public async void CreateUser_Fail()
        {
           
          
            UserDTO user = new UserDTO
            {
                FirstName = "FTest",
                LastName = "LTest",
                Dob = new DateOnly(1993, 12, 8),
                Email = "FTestLTest@gmail.com",
                PhoneNumber = "6467393399",
                Address = "Test street",
                City = "Test City",
                State = "Test State",
                Country = "Test Country",
                PostalCode = "66367",
                IsActive = true
            };

            Mock<IUserService> _mockedUserService = new Mock<IUserService>();
            _mockedUserService.Setup(x => x.CreateUser(user)).Returns(Task.FromResult(new UserDTO()));

            UserController _mockedUserController = new UserController(_mockedUserService.Object);
            var response = await _mockedUserController.CreateUser(user);
            Assert.False(response.IsSuccess);
            Assert.Equal(0, ((UserDTO)response.Result).UserId);
        }


        [Fact]
        public async void UpdateUser_Success()
        {
            UserDTO user = new UserDTO
            {
                UserId = 1,
                FirstName = "FTest",
                LastName = "LTest",
                Dob = new DateOnly(1993, 12, 8),
                Email = "FTestLTest@gmail.com",
                PhoneNumber = "6467393399",
                Address = "Test street",
                City = "Test City",
                State = "Test State",
                Country = "Test Country",
                PostalCode = "66367",
                IsActive = true
            };

            var response = await _userController.UpdateUser(user);
            Assert.True(response.IsSuccess);
            Assert.Equal( user.FirstName, ((UserDTO)response.Result).FirstName);
        }


        [Fact]
        public async void UpdateUser_Fail()
        {

            UserDTO user = new UserDTO
            {
                UserId=1,
                FirstName = "FTest",
                LastName = "LTest",
                Dob = new DateOnly(1993, 12, 8),
                Email = "FTestLTest@gmail.com",
                PhoneNumber = "6467393399",
                Address = "Test street",
                City = "Test City",
                State = "Test State",
                Country = "Test Country",
                PostalCode = "66367",
                IsActive = true
            };

            Mock<IUserService> _mockedUserService = new Mock<IUserService>();
            _mockedUserService.Setup(x => x.UpdateUser(user)).Throws(new Exception());

            UserController _mockedUserController = new UserController(_mockedUserService.Object);
            var response = await _mockedUserController.UpdateUser(user);
            Assert.False(response.IsSuccess);
            Assert.Equal("There is some Exception", response.StatusMessage);
        }

        [Fact]
        public async void DeleteUser_Success()
        {
            int userId = 2;
            var response = await _userController.DeleteUser(userId);
            Assert.True(response.IsSuccess);
            Assert.Equal(string.Format(Constants.DEL_USER_SUCCESS, userId), response.StatusMessage);

        }


        [Fact]
        public async void DeleteUser_Fail()
        {
            int userId = 100;
            var response = await _userController.DeleteUser(userId);
            Assert.False(response.IsSuccess);
            Assert.Equal(string.Format(Constants.USER_NOT_EXISTS, userId), response.StatusMessage);
        }

        [Fact]
        public async void GetUserById_Success()
        {
            int userId = 1;
            var response = await _userController.GetUserById(userId);
            Assert.True(response.IsSuccess);
            Assert.Equal(userId, ((UserDTO)response.Result).UserId);

        }


        [Fact]
        public async void GetUserById_Fail()
        {
            int userId = 100;
            var response = await _userController.GetUserById(userId);
            Assert.False(response.IsSuccess);
            Assert.Equal(0, ((UserDTO)response.Result).UserId);
        }

        [Fact]
        public async void GetAllUsers_Success()
        {
            var response = await _userController.GetAllUsers();
            Assert.True(response.IsSuccess);
            Assert.True(((List<UserDTO>)response.Result).Count() > 0);

        }


        [Fact]
        public async void GetAllUsers_Fail()
        {
            Mock<IUserService> _mockedUserService = new Mock<IUserService>();
            _mockedUserService.Setup(x => x.GetAllUsers()).Returns(Task.FromResult(new List<UserDTO>()));
            UserController _mockedUserController = new UserController(_mockedUserService.Object);
            var response = await _mockedUserController.GetAllUsers();
            Assert.False(response.IsSuccess);
            Assert.True(((List<UserDTO>)response.Result).Count() == 0);
        }




    }
}
