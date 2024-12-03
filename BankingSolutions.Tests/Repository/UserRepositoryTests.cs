using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSolution.Controllers;
using BankingSolution.DTO;
using BankingSolution.Models;
using BankingSolution.Repository;

namespace BankingSolutions.Tests.Repository
{
    public class UserRepositoryTests
    {
        private readonly IUserRepository _userRepository;

        public UserRepositoryTests()
        {
            _userRepository = new UserRepository();
        }

        [Fact]
        public async void CreateUser_Success()
        {
            UserModel user = new UserModel
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

            var response = await _userRepository.CreateUser(user);
            Assert.True(response.UserId > 0);
            
        }

        [Fact]
        public async void DeleteUser_Success()
        {            
            var response = await _userRepository.DeleteUser(3);
            Assert.True(response.Item1);

        }

        [Fact]
        public async void DeleteUser_Fail()
        {
            var response = await _userRepository.DeleteUser(1000);
            Assert.False(response.Item1);

        }

        [Fact]
        public async void GetAllUser_Success()
        {
            var response = await _userRepository.GetAllUsers();
            Assert.True(response.Count() > 0);

        }

        [Fact]
        public async void GetUserById_Success()
        {
            var response = await _userRepository.GetUserById(1);
            Assert.Equal(1, response.UserId);

        }

        [Fact]
        public async void GetUserById_Fail()
        {
            var response = await _userRepository.GetUserById(1000);
            Assert.Equal(0, response.UserId);

        }

        [Fact]
        public async void UpdateUser_Success()
        {
            UserModel user = new UserModel
            {
                UserId = 1,
                FirstName = "Hello",
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

            var response = await _userRepository.UpdateUser(user);
            Assert.Equal( "Hello", response.FirstName);

        }


    }
}
