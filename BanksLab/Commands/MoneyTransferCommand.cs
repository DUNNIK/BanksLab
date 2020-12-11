namespace BanksLab.Commands
{
    public class MoneyTransferCommand : BanksLab.CompositeBankAccountCommand
    {
        public MoneyTransferCommand(BankAccount from,
            BankAccount to, int amount)
        {
            AddRange(new[]
            {
                new BanksLab.BankAccountCommand(from,
                    BanksLab.BankAccountCommand.Action.Withdraw, amount),
                new BanksLab.BankAccountCommand(to,
                    BanksLab.BankAccountCommand.Action.Deposit, amount)
            });
        }

        public override void Call()
        {
            BanksLab.BankAccountCommand last = null;
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