using System.Collections.Generic;
using System.Linq;

namespace BanksLab.Commands
{
    public class CompositeBankAccountCommand
        : List<ICommand>, ICommand
    {
        public CompositeBankAccountCommand()
        {
        }

        public CompositeBankAccountCommand(IEnumerable<ICommand> collection)
            : base(collection)
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
                ((IEnumerable<ICommand>) this).Reverse())
                cmd.Undo();
        }

        public bool Success { get; set; }
    }
}