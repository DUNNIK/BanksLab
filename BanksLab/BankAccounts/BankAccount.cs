using System;

namespace BanksLab.BankAccounts
{
    public abstract class BankAccount
    {
        private Client.Client Client;
        protected readonly DateTime CreateTime = SystemTime.Now.Invoke();
        public bool StopAddPercents = false;
        public bool StopRemoveCommission = false;
        protected double Balance;
        protected int OverdraftLimit = 0;
        private int BankLimitAmount;
        protected double PercentOnAccount;
        public BankAccount(Client.Client client, int bankLimitAmount, int balance = 0)
        {
            Client = client;
            BankLimitAmount = bankLimitAmount;
            Balance = balance;
        }

        public void Deposit(double amount)
        {
            if (CheckingForNotValidateAccount(amount)) return;
            Balance += amount;
        }

        protected bool CheckingForNotValidateAccount(double amount)
        {
            return !Client.IsValidate() && amount >= BankLimitAmount;
        }
        public abstract bool Withdraw(double amount);

        public void Transfer(BankAccount to, double amount)
        {
            if (CheckingForNotValidateAccount(amount)) return;
            var translation = new Commands.MoneyTransferCommand(this, to, amount);
        }

        public override string ToString()
        {
            return $"Your Balance: {Balance}";
        }
    }
}