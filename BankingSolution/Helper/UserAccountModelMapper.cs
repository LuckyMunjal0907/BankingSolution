using BankingSolution.DTO;
using BankingSolution.Models;

namespace BankingSolution.Helper
{
    public static class UserAccountModelMapper
    {
        public static UserAccountDTO ModelToDTO(UserAccountModel model)
        {
            return new UserAccountDTO
            {
                AccountNumber = model.AccountNumber,
                UserId = model.UserId,
                AccountType = model.AccountType,
                Balance = model.Balance,
                IsActive = model.IsActive
            };
        }

        public static UserAccountModel DTOToModel(UserAccountDTO model)
        {
            return new UserAccountModel
            {
                AccountNumber = model.AccountNumber,
                UserId = model.UserId,
                AccountType = model.AccountType,
                Balance = model.Balance,
                IsActive = model.IsActive
            };
        }
    }
}
