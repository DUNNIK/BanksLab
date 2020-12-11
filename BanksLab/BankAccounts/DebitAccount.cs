using System;
using static System.Console;

namespace BanksLab.BankAccounts
{
    public class DebitAccount : BankAccount
    {
        public override void Deposit(int amount)
        {
            balance += amount;
            WriteLine($"Deposited ${amount}, balance is now {balance}");
        }

        public override bool Withdraw(int amount)
        {
            if (balance - amount < overdraftLimit) return false;
            balance -= amount;
            WriteLine($"Withdrew ${amount}, balance is now {balance}");
            return true;
        }

        public override void Transfer(BankAccount to)
        {
            throw new NotImplementedException();
        }
    }
}