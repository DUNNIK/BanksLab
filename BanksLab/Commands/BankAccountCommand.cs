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

        public void Call()
        {
            switch (_action)
            {
                case Action.Deposit:
                    _account.Deposit(_amount);
                    Success = true;
                    break;
                case Action.Withdraw:
                    Success = _account.Withdraw(_amount);
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