using BankingSolution.Models;

namespace BankingSolution.Repository
{
    public interface IUserRepository
    {
        Task<UserModel> CreateUser(UserModel user);
        Task<UserModel> UpdateUser(UserModel user);
        Task<IEnumerable<UserModel>> GetAllUsers();
        Task<UserModel> GetUserById(int id);
        Task<Tuple<bool, string>> DeleteUser(int id);
    }
}
