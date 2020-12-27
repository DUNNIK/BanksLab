using System.Collections.Generic;
using BanksLab.BankAccounts;

namespace BanksLab.Bank
{
    public class Bank
    {
        protected int NotValidateSum;
        public readonly Dictionary<string, BankAccount> Accounts = new Dictionary<string, BankAccount>();
        public BankAccountFactory CreateBankAccount => new BankAccountFactory(NotValidateSum, Accounts);

        public Bank(int notValidateSum)
        {
            NotValidateSum = notValidateSum;
        }

        protected Bank(int notValidateSum, Dictionary<string, BankAccount> accounts)
        {
            NotValidateSum = notValidateSum;
            Accounts = accounts;
        }
    }
}