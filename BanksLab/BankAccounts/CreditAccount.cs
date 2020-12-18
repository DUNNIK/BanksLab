using System;
using System.Threading.Tasks;

namespace BanksLab.BankAccounts
{
    public class CreditAccount : BankAccount
    {
        private readonly int _commission;

        public CreditAccount(int overdraftLimit, int commission)
        {
            OverdraftLimit = overdraftLimit;
            _commission = commission;
            RemoveCommission();
        }

        public override bool Withdraw(double amount)
        {
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
}