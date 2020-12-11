namespace BanksLab.Commands
{
    public interface ICommand
    {
        bool Success { get; set; }
        void Call();
        void Undo();
    }
}