namespace BankingSolution.Utils
{
    public static class Constants
    {
        public const string DEL_USER_SUCCESS = "User {0} has been deleted.";
        public const string USER_NOT_EXISTS = "User {0} does not exists.";
        public const string DEL_USER_ALLAC_MSG_SUCCESS = "Accounts has been deleted for User: {0}.";
        public const string DEL_USER_ALLAC_MSG_FAILURE = "No Active Accounts found  for User: {0}.";
        public const string DEL_USER_AC_MSG_SUCCESS = "Accounts with Account Number: {0} has been deleted.";
        public const string DEL_USER_AC_MSG_FAILURE = "Account Number {0} is already deleted.";
        public const string ACC_DEPOSIT_VALIDATION = "You cannot deposit more than ${0} in one transaction.";
        public const string ACC_DEPOSIT_SUCCESS = "${0} has been deposited into your account with account number {1}. Your updated balance is ${2}.";
        public const string ACC_NOT_EXISTS = "Account Number {0} does not exists.";
        public const string ACC_CHK_BALANCE = "Balance in your account number {0} is ${1}.";
        public const string INSUFF_BALANCE_AC = "You don't have sufficient balance in your account {0}.";
        public const string WITHDRAW_MAX_LIMIT = "You cannot withdraw more than {0}% of your balance in one transaction.";
        public const string WITHDRAW_MIN_BALANCE = "You cannot withdraw ${0} from your account {1} as your minimum balance should be ${2} after withdrawl.";
        public const string WITHDRAW_SUCCESS = "You have withdrwan ${0} from your account {1}. Your remaning balance is ${2}.";
    }
}
