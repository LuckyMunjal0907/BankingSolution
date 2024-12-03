using BankingSolution.DTO;
using BankingSolution.Models;

namespace BankingSolution.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> CreateUser(UserDTO user);
        Task<UserDTO> UpdateUser(UserDTO user);
        Task<List<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserById(int id);
        Task<Tuple<bool, string>> DeleteUser(int id);
    }
}
