using System;
using System.Threading.Tasks;

namespace BanksLab.BankAccounts
{
    public class DebitAccount : BankAccount
    {
        private DateTime _lastPercentsTime = SystemTime.Now.Invoke();
        private DateTime _lastChargeTime = SystemTime.Now.Invoke();
        private double _monthPercents;
        public DebitAccount(Client.Client client, int bankLimitAmount, DebitAccountInformation information) : base(client, bankLimitAmount, information.Balance)
        {
            PercentOnAccount = information.PercentOnAccount;
            //AddPercents();
        }

        public override bool Withdraw(double amount)
        {
            if (CheckingForNotValidateAccount(amount)) return false;
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
            while (MonthCondition1())
            {
                _lastChargeTime = _lastChargeTime.AddMonths(1);
                Balance += MonthPercents();
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
        }
        private bool DayCondition()
        {
            return (SystemTime.Now.Invoke() - _lastPercentsTime).Days >= 1;
        }
        private bool MonthCondition()
        {
            return (_lastPercentsTime - _lastChargeTime).Days == 31;
        }
        private bool MonthCondition1()
        {
            return (SystemTime.Now.Invoke() - _lastChargeTime).Days >= 31;
        }

        private bool MonthMinusCondition()
        {
            return (SystemTime.Now.Invoke() - _lastChargeTime).Days <= -31;
        }
    }
    public class DebitAccountInformation
    {
        public readonly double PercentOnAccount;
        public readonly int Balance;

        public DebitAccountInformation(double percentOnAccount, int balance = 0)
        {
            PercentOnAccount = percentOnAccount;
            Balance = balance;
        }
    }
}