using System;
using System.Threading.Tasks;

namespace BanksLab.BankAccounts
{
    public class CreditAccount : BankAccount
    {
        private readonly int _commission;

        public CreditAccount(Client.Client client, int bankLimitAmount, CreditAccountInformation information) : base(client, bankLimitAmount, information.Balance)
        {
            OverdraftLimit = information.OverdraftLimit;
            _commission = information.Commission;
            RemoveCommission();
        }

        public override bool Withdraw(double amount)
        {
            if (CheckingForNotValidateAccount(amount)) return false;
            if (!(Balance - amount >= OverdraftLimit)) return false;
            Balance -= amount;
            return true;
        }

        private async void RemoveCommission()
        {
            await Task.Run(() =>
            {
                while(!StopRemoveCommission)
                {
                    if (!MinusBalance() && !MonthCondition()) continue;
                    Balance -= _commission;
                }
            });
        }

        private bool MinusBalance()
        {
            return Balance < 0;
        }
        private bool MonthCondition()
        {
            return (DateTime.Now - CreateTime).Days % 31 == 0;
        }
    }

    public class CreditAccountInformation
    {
        public readonly int OverdraftLimit;
        public readonly int Commission;
        public readonly int Balance;

        public CreditAccountInformation(int overdraftLimit, int commission, int balance = 0)
        {
            OverdraftLimit = overdraftLimit;
            Commission = commission;
            Balance = balance;
        }
    }
}