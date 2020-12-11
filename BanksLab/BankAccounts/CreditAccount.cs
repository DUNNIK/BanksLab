using System;

namespace BanksLab.BankAccounts
{
    public class CreditAccount : BankAccount
    {
        
        public override bool Withdraw(double amount)
        {
            throw new NotImplementedException();
        }

        public override void Transfer(BankAccount to, double amount)
        {
            throw new NotImplementedException();
        }
    }
}