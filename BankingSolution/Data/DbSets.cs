using BankingSolution.Models;

namespace BankingSolution.Data
{
    public static class DbSets
    {
        public static List<UserModel> users = new List<UserModel> { new UserModel { 
                                                                    UserId = 1, FirstName = "Test1", LastName = "User1", Dob = new DateOnly(1988, 07, 09), 
                                                                    Email ="Test1User1@gmail.com", PhoneNumber ="9999999999", Address="Test Street1", City ="Test1City", 
                                                                    State="Test1State", PostalCode="ZZZZ", Country ="TestCountry" },
                                                                    new UserModel {
                                                                    UserId = 2, FirstName = "Test2", LastName = "User2", Dob = new DateOnly(1990, 05, 12),
                                                                    Email ="Test2User2@gmail.com", PhoneNumber ="8888888888", Address="Test Street2", City ="Test2City",
                                                                    State="Test2State", PostalCode="YYYY", Country ="TestCountry" },
                                                                     new UserModel {
                                                                    UserId = 3, FirstName = "Test3", LastName = "User3", Dob = new DateOnly(1983, 09, 09),
                                                                    Email ="Test3User3@gmail.com", PhoneNumber ="77777777", Address="Test Street3", City ="Test3City",
                                                                    State="Test3State", PostalCode="YYYY", Country ="TestCountry" },
                                                                    new UserModel {
                                                                    UserId = 4, FirstName = "Test4", LastName = "User4", Dob = new DateOnly(1974, 01, 01),
                                                                    Email ="Test4User4@gmail.com", PhoneNumber ="666666666", Address="Test Street4", City ="Test4City",
                                                                    State="Test4State", PostalCode="YYYY", Country ="TestCountry" },
                                                                    new UserModel {
                                                                    UserId = 5, FirstName = "Test5", LastName = "User5", Dob = new DateOnly(1947, 08, 15),
                                                                    Email ="Test5User5@gmail.com", PhoneNumber ="555555555", Address="Test Street5", City ="Test5City",
                                                                    State="Test5State", PostalCode="YYYY", Country ="TestCountry" },
                                                                    new UserModel {
                                                                    UserId = 6, FirstName = "Test6", LastName = "User6", Dob = new DateOnly(1957, 06, 27),
                                                                    Email ="Test6User6@gmail.com", PhoneNumber ="444444444", Address="Test Street6", City ="Test6City",
                                                                    State="Test6State", PostalCode="YYYY", Country ="TestCountry" }};

        public static List<UserAccountModel> userAccounts = new List<UserAccountModel> { 
                                                                    new UserAccountModel {
                                                                    UserId = 1, AccountNumber = 10000000, AccountType = Utils.AccountTypes.Savings, Balance = 100000 },
                                                                    new UserAccountModel {
                                                                    UserId = 1, AccountNumber = 10000004, AccountType = Utils.AccountTypes.Savings, Balance = 40000 },
                                                                     new UserAccountModel {
                                                                    UserId = 1, AccountNumber = 10000001, AccountType = Utils.AccountTypes.Current, Balance = 40000 },
                                                                     new UserAccountModel {
                                                                    UserId = 2, AccountNumber = 10000002, AccountType = Utils.AccountTypes.Savings, Balance = 3000 },
                                                                    new UserAccountModel {
                                                                    UserId = 2, AccountNumber = 10000003, AccountType = Utils.AccountTypes.Savings, Balance = 300 },
                                                                    new UserAccountModel {
                                                                    UserId = 3, AccountNumber = 10000005, AccountType = Utils.AccountTypes.Savings, Balance = 3000 },
                                                                    new UserAccountModel {
                                                                    UserId = 4, AccountNumber = 10000006, AccountType = Utils.AccountTypes.Savings, Balance = 300 },
                                                                    new UserAccountModel {
                                                                    UserId = 5, AccountNumber = 10000007, AccountType = Utils.AccountTypes.Savings, Balance = 40000 },
                                                                    new UserAccountModel {
                                                                    UserId = 4, AccountNumber = 10000008, AccountType = Utils.AccountTypes.Savings, Balance = 1300 },
                                                                    new UserAccountModel {
                                                                    UserId = 6, AccountNumber = 10000009, AccountType = Utils.AccountTypes.Savings, Balance = 3000 },
                                                                    new UserAccountModel {
                                                                    UserId = 4, AccountNumber = 10000010, AccountType = Utils.AccountTypes.Savings, Balance = 5000 }};
                                                                     
    }
}
