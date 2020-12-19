using System.Collections.Generic;
using BanksLab.BankAccounts;

namespace BanksLab.Bank
{
    public abstract class Bank
    {
        public int NotValidateSum;
        public Dictionary<string, BankAccount> Accounts = new Dictionary<string, BankAccount>();
        public BankAccountFactory CreateBankAccount => new BankAccountFactory(this);

        public Bank(int notValidateSum)
        {
            NotValidateSum = notValidateSum;
        }

        protected Bank(Bank bank)
        {
            Accounts = bank.Accounts;
        }
    }
}