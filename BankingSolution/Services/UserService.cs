using System.Runtime.InteropServices;
using BankingSolution.DTO;
using BankingSolution.Helper;
using BankingSolution.Models;
using BankingSolution.Repository;
using BankingSolution.Services.Interfaces;

namespace BankingSolution.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        public UserService(IUserRepository userRepo) { 
            _userRepo = userRepo; 
        }

       public async Task<UserDTO> CreateUser(UserDTO user)
        {           
            return  UserModelMapper.ModelToDTO(await _userRepo.CreateUser(UserModelMapper.DTOToModel(user)));    

        }

        public async Task<Tuple<bool, string>> DeleteUser(int id)
        {
           return await _userRepo.DeleteUser(id);
        }

       public async Task<List<UserDTO>> GetAllUsers()
        {
            var users= new List<UserDTO>();
            foreach (var item in await _userRepo.GetAllUsers())
            {
                users.Add(UserModelMapper.ModelToDTO(item));
            };

            return users;
        }

       public async Task<UserDTO> GetUserById(int id)
        {
            return UserModelMapper.ModelToDTO(await _userRepo.GetUserById(id));
        }

        public async Task<UserDTO> UpdateUser(UserDTO user)
        {
            return UserModelMapper.ModelToDTO(await _userRepo.UpdateUser(UserModelMapper.DTOToModel(user)));
        }
    }
}
