using System;
using System.Threading.Tasks;

namespace BanksLab.BankAccounts
{
    public class DepositAccount : BankAccount
    {
        private readonly DateTime _depositEndDate;
        private DateTime _lastPercentsTime = DateTime.Now;
        private double _monthPercents;
        public DepositAccount(DateTime depositEndDate, int balance = 0) : base(balance)
        {
            _depositEndDate = depositEndDate;
            Percent = FindingPercent();
            AddPercents();
        }

        private double FindingPercent()
        {
            if (FirstRange())
            {
                return 3;
            }

            if (SecondRange())
            {
                return 3.5;
            }

            if (ThirdRange())
            {
                return 4;
            }

            return 0;
        }

        private bool FirstRange()
        {
            return Balance < 50000;
        }
        private bool SecondRange()
        {
            return Balance <= 100000;
        }
        private bool ThirdRange()
        {
            return Balance > 100000;
        }
        public override bool Withdraw(double amount)
        {
            if (_depositEndDate > DateTime.Now) return false;
            if (Balance - amount < OverdraftLimit) return false;
            Balance -= amount;
            return true;
        }
        protected async void AddPercents()
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
            Percent = FindingPercent();
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