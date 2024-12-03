using System.Diagnostics.Metrics;
using System.Net;
using BankingSolution.DTO;
using BankingSolution.Models;

namespace BankingSolution.Helper
{
    public static class UserModelMapper
    {
        public static UserDTO ModelToDTO(UserModel user)
        {
            return new UserDTO
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Dob = user.Dob,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                City = user.City,
                State = user.State,
                PostalCode = user.PostalCode,
                Country = user.Country

            };
        }

        public static UserModel DTOToModel(UserDTO user)
        {
            return new UserModel
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Dob = user.Dob,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                City = user.City,
                State = user.State,
                PostalCode = user.PostalCode,
                Country = user.Country

            };
        }
    }
}
