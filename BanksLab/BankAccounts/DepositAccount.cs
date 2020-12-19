using System;
using System.Threading.Tasks;

namespace BanksLab.BankAccounts
{
    public class DepositAccount : BankAccount
    {
        private readonly DateTime _depositEndDate;
        private DateTime _lastPercentsTime = DateTime.Now;
        private double _monthPercents;
        public DepositAccount(Client.Client client, int bankLimitAmount, DepositAccountInformation information) : base(client, bankLimitAmount, information.Balance)
        {
            _depositEndDate = information.DepositEndDate;
            PercentOnAccount = FindingPercent();
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
            if (CheckingForNotValidateAccount(amount)) return false;
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
            _monthPercents += Balance * (PercentOnAccount / 365);
        }
        private void AddPercentsForMonth()
        {
            if (!MonthCondition()) return;
            Balance += _monthPercents;
            _monthPercents = 0;
            PercentOnAccount = FindingPercent();
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
    public class DepositAccountInformation
    {
        public readonly DateTime DepositEndDate;
        public readonly int Balance;

        public DepositAccountInformation(DateTime depositEndDate, int balance = 0)
        {
            DepositEndDate = depositEndDate;
            Balance = balance;
        }
    }
}