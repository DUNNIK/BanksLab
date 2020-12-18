using System;
using System.Threading.Tasks;

namespace BanksLab.BankAccounts
{
    public abstract class BankAccount
    {
        protected readonly DateTime CreateTime = DateTime.Now;
        public bool StopAddPercents = false;
        public bool StopRemoveCommission = false;
        protected double Balance;
        protected int OverdraftLimit = 0;
        protected double Percent;
        public BankAccount(int balance = 0)
        {
            Balance = balance;
        }

        public void Deposit(double amount)
        {
            Balance += amount;
        }
        

        public abstract bool Withdraw(double amount);

        public void Transfer(BankAccount to, double amount)
        {
            var translation = new Commands.MoneyTransferCommand(this, to, amount);
        }
        
    }
}