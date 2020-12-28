using BanksLab.BankAccounts;
using BanksLab.Exceptions;

namespace BanksLab.Commands
{
    public class BankAccountCommand : ICommand
    {
        public enum Action
        {
            Deposit,
            Withdraw
        }

        private readonly BankAccount _account;
        private readonly Action _action;
        private readonly double _amount;

        public BankAccountCommand(BankAccount account, Action action, double amount)
        {
            _account = account;
            _action = action;
            _amount = amount;
        }

        private void PercentsAndCommissionAction()
        {
            _account.AddMonthPercents();
            _account.RemoveMonthCommission();
        }
        public void Call()
        {
            switch (_action)
            {
                case Action.Deposit:
                    PercentsAndCommissionAction();
                    _account.Deposit(_amount);
                    Success = true;
                    PercentsAndCommissionAction();
                    break;
                case Action.Withdraw:
                    PercentsAndCommissionAction();
                    Success = _account.Withdraw(_amount);
                    PercentsAndCommissionAction();
                    break;
                default:
                    throw new ActionArgumentsException();
            }
        }

        public void Undo()
        {
            if (!Success) return;
            switch (_action)
            {
                case Action.Deposit:
                    _account.Withdraw(_amount);
                    break;
                case Action.Withdraw:
                    _account.Deposit(_amount);
                    break;
                default:
                    throw new ActionArgumentsException();
            }
        }

        public bool Success { get; set; }
    }
}