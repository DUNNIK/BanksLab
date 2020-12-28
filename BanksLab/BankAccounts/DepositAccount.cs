using System;
using System.Threading.Tasks;

namespace BanksLab.BankAccounts
{
    public class DepositAccount : BankAccount
    {
        private readonly DateTime _depositEndDate;
        private DateTime _lastPercentsTime = SystemTime.Now.Invoke();
        private DateTime _lastChargeTime = SystemTime.Now.Invoke();
        private double _monthPercents;
        public DepositAccount(Client.Client client, int bankLimitAmount, DepositAccountInformation information) : base(client, bankLimitAmount, information.Balance)
        {
            _depositEndDate = information.DepositEndDate;
            PercentOnAccount = FindingPercent();
            //AddPercents();
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
            if (_depositEndDate > SystemTime.Now.Invoke()) return false;
            if (Balance - amount < OverdraftLimit) return false;
            Balance -= amount;
            return true;
        }

        public override void AddMonthPercents()
        {
            if (SystemTime.Now.Invoke() > _lastChargeTime)
            {
                PlusPercentsAction();
            }
            else
            {
                MinusPercentsAction();
            }
        }

        public override void RemoveMonthCommission()
        {
            Balance -= 0;
        }

        private void PlusPercentsAction()
        {
            while (MonthCondition())
            {
                _lastChargeTime = _lastChargeTime.AddMonths(1);
                Balance += MonthPercents();
                PercentOnAccount = FindingPercent();
            }
        }

        private double MonthPercents()
        {
            return Balance * (PercentOnAccount / 12) / 100;
        }
        private void MinusPercentsAction()
        {
            while (MonthMinusCondition())
            {
                _lastChargeTime = _lastChargeTime.AddMonths(-1);
                Balance -= MonthPercents();
                PercentOnAccount = FindingPercent();
            }
        }
        private async void AddPercents()
        {
            await Task.Run(() =>
            {
                while(!StopAddPercents)
                {
                    UpdateDailyInformation();
                    AddPercentsForMonth();
                }
            });
        }

        private void UpdateDailyInformation()
        {
            if (!DayCondition()) return;
            _lastPercentsTime = _lastPercentsTime.AddDays(1);
            _monthPercents += Balance * (PercentOnAccount / 365);
        }
        private void AddPercentsForMonth()
        {
            if (!MonthCondition()) return;
            _lastChargeTime = _lastChargeTime.AddMonths(1);
            Balance += _monthPercents;
            _monthPercents = 0;
            PercentOnAccount = FindingPercent();
        }
        private bool DayCondition()
        {
            return (SystemTime.Now.Invoke() - _lastPercentsTime).Days >= 1;
        }
        private bool MonthCondition()
        {
            return (_lastPercentsTime - _lastChargeTime).Days == 31;
        }
        private bool MonthMinusCondition()
        {
            return (SystemTime.Now.Invoke() - _lastChargeTime).Days <= -31;
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