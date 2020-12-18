using System;
using System.Threading.Tasks;
using static System.Console;

namespace BanksLab.BankAccounts
{
    public class DebitAccount : BankAccount
    {
        private DateTime _lastPercentsTime = DateTime.Now;
        private double _monthPercents;
        public DebitAccount(double percent, int balance = 0) : base(balance)
        {
            Percent = percent;
            AddPercents();
        }

        public override bool Withdraw(double amount)
        {
            if (Balance - amount < OverdraftLimit) return false;
            Balance -= amount;
            return true;
        }

        private async void AddPercents()
        {
            await Task.Run(() =>
            {
                while(!StopAddPercents)
                {
                    if (!DayCondition()) continue;
                    UpdateDailyInformation();
                    AddPercentsForMonth();
                }
            });
        }

        private void UpdateDailyInformation()
        {
            _lastPercentsTime = DateTime.Now;
            _monthPercents += Balance * (Percent / 365);
        }
        private void AddPercentsForMonth()
        {
            if (!MonthCondition()) return;
            Balance += _monthPercents;
            _monthPercents = 0;
        }
        private bool DayCondition()
        {
            return (DateTime.Now - _lastPercentsTime).Hours == 24;
        }
        private bool MonthCondition()
        {
            return (DateTime.Now - CreateTime).Days % 31 == 0;
        }
    }
}