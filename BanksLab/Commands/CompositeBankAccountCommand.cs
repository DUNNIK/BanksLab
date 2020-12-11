using System.Collections.Generic;
using System.Linq;

namespace BanksLab.Commands
{
    public class CompositeBankAccountCommand
        : List<BankAccountCommand>, ICommand
    {
        public CompositeBankAccountCommand()
        {
        }

        public CompositeBankAccountCommand(IEnumerable<BankAccountCommand> collection) : base(collection)
        {
        }

        public virtual void Call()
        {
            Success = true;
            ForEach(cmd =>
            {
                cmd.Call();
                Success &= cmd.Success;
            });
        }

        public virtual void Undo()
        {
            foreach (var cmd in
                ((IEnumerable<BankAccountCommand>) this).Reverse())
                cmd.Undo();
        }

        public bool Success { get; set; }
    }
}