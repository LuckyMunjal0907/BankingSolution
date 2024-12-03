using System.Reflection.Metadata;
using BankingSolution.Data;
using BankingSolution.Models;
using BankingSolution.Utils;

namespace BankingSolution.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task<UserModel> CreateUser(UserModel user)
        {
            var maxUserId =  await Task.Run(()=>DbSets.users.Max(x => x.UserId));
            user.UserId = maxUserId + 1;
            await Task.Run(()=> DbSets.users.Add(user));
            return user; 
        }

        public async  Task<Tuple<bool, string>> DeleteUser(int id)
        {
            var user = await Task.Run(() => DbSets.users.FirstOrDefault(x => x.UserId == id && x.IsActive));
                if (user != null)
                {
                    user.IsActive = false;
                    return Tuple.Create(true, string.Format(Constants.DEL_USER_SUCCESS, id));
                }
                else
                    return Tuple.Create(false, string.Format(Constants.USER_NOT_EXISTS, id));
            
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            return await Task.Run(()=> DbSets.users.Where(x=> x.IsActive));
        }

        public async Task<UserModel> GetUserById(int id)
        {

            var user = await Task.Run(() => DbSets.users.SingleOrDefault(x => x.UserId == id && x.IsActive));
            if (user != null)
            {
                return user;
            }
            else
                return new UserModel();
        }

        public async Task<UserModel> UpdateUser(UserModel user)
        {
            await Task.Run(() => { var userModel =DbSets.users.FirstOrDefault(x => x.UserId == user.UserId && x.IsActive);
                if (userModel != null)
                    userModel = user;
            });
            return user;
        }
    }
}
