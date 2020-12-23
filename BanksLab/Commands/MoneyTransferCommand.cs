using BanksLab.BankAccounts;

namespace BanksLab.Commands
{
    public class MoneyTransferCommand : CompositeBankAccountCommand
    {
        public MoneyTransferCommand(BankAccount from,
            BankAccount to, double amount)
        {
            AddRange(new[]
            {
                new BankAccountCommand(from,
                    BankAccountCommand.Action.Withdraw, amount),
                new BankAccountCommand(to,
                    BankAccountCommand.Action.Deposit, amount)
            });
        }

        public override void Call()
        {
            ICommand last = null;
            foreach (var cmd in this)
                if (last == null || last.Success)
                {
                    cmd.Call();
                    last = cmd;
                }
                else
                {
                    cmd.Undo();
                    break;
                }
        }
    }
}