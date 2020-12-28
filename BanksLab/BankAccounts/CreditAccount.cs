using System;
using System.Threading.Tasks;

namespace BanksLab.BankAccounts
{
    public class CreditAccount : BankAccount
    {
        private readonly int _commission;
        private DateTime _lastCommissionTime = SystemTime.Now.Invoke();
        public CreditAccount(Client.Client client, int bankLimitAmount, CreditAccountInformation information) : base(client, bankLimitAmount, information.Balance)
        {
            OverdraftLimit = information.OverdraftLimit;
            _commission = information.Commission;
            //RemoveCommission();
        }

        public override bool Withdraw(double amount)
        {
            if (CheckingForNotValidateAccount(amount)) return false;
            if (!(Balance - amount >= OverdraftLimit)) return false;
            Balance -= amount;
            return true;
        }

        public override void AddMonthPercents()
        {
            Balance += 0;
        }

        public override void RemoveMonthCommission()
        {
            if (SystemTime.Now.Invoke() > _lastCommissionTime)
            {
                PlusCommissionAction();
            }
            else
            {
                MinusCommissionAction();
            }
        }

        private void PlusCommissionAction()
        {
            while (MinusBalance() && MonthCondition())
            {
                _lastCommissionTime = _lastCommissionTime.AddMonths(1);
                Balance -= _commission;
            }
        }

        private void MinusCommissionAction()
        {
            while (MinusBalance() && MonthMinusCondition())
            {
                _lastCommissionTime = _lastCommissionTime.AddMonths(-1);
                Balance += _commission;
            }
        }
        private async void RemoveCommission()
        {
            await Task.Run(() =>
            {
                while(!StopRemoveCommission)
                {
                    if (MinusBalance() && MonthCondition())
                    {
                        _lastCommissionTime = _lastCommissionTime.AddMonths(1);
                        Balance -= _commission;
                    }
                }
            });
        }

        private bool MinusBalance()
        {
            return Balance < 0;
        }
        private bool MonthCondition()
        {
            return (SystemTime.Now.Invoke() - _lastCommissionTime).Days >= 31;
        }
        private bool MonthMinusCondition()
        {
            return (SystemTime.Now.Invoke() - _lastCommissionTime).Days <= -31;
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