using System.Collections.Generic;
using BanksLab.BankAccounts;

namespace BanksLab.Bank
{
    public abstract class Bank
    {
        protected readonly int NotValidateSum;
        public readonly Dictionary<string, BankAccount> Accounts = new Dictionary<string, BankAccount>();
        public BankAccountFactory CreateBankAccount => new BankAccountFactory(this);

        protected Bank(int notValidateSum)
        {
            NotValidateSum = notValidateSum;
        }

        protected Bank(Bank bank)
        {
            Accounts = bank.Accounts;
        }
    }
}