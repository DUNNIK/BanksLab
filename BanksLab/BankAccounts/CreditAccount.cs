using System;

namespace BanksLab.BankAccounts
{
    public class CreditAccount : BankAccount
    {
        public override void Deposit(int amount)
        {
            throw new NotImplementedException();
        }

        public override bool Withdraw(int amount)
        {
            throw new NotImplementedException();
        }

        public override void Transfer(BankAccount to)
        {
            throw new NotImplementedException();
        }
    }
}