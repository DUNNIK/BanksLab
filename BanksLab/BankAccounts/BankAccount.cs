using System;
using System.Linq;
using BanksLab.Commands;

namespace BanksLab.BankAccounts
{
    public abstract class BankAccount
    {
        private readonly Client.Client _client;
        protected readonly DateTime CreateTime = SystemTime.Now.Invoke();
        public bool StopAddPercents = false;
        public bool StopRemoveCommission = false;
        protected double Balance;
        protected int OverdraftLimit = 0;
        private readonly int _bankLimitAmount;
        protected double PercentOnAccount;
        private readonly CompositeBankAccountCommand _accountCommandsHistory = new CompositeBankAccountCommand();

        protected BankAccount(Client.Client client, int bankLimitAmount, int balance = 0)
        {
            _client = client;
            _bankLimitAmount = bankLimitAmount;
            Balance = balance;
        }

        public void Deposit(double amount)
        {
            if (CheckingForNotValidateAccount(amount)) return;
            Balance += amount;
        }

        protected bool CheckingForNotValidateAccount(double amount)
        {
            return _client.IsDoubtful() && amount >= _bankLimitAmount;
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

        public void UpdateAccountHistory(ICommand newCommand)
        {
            _accountCommandsHistory.Add(newCommand);
        }

        public void RestorePreviousAccountState()
        {
            var command = _accountCommandsHistory.Last();
            _accountCommandsHistory.Remove(command);
            command.Undo();
        }
    }
}