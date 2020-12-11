namespace BanksLab.BankAccounts
{
    public abstract class BankAccount
    {
        protected int balance;
        protected int overdraftLimit = 0;

        public BankAccount(int balance = 0)
        {
            this.balance = balance;
        }

        public abstract void Deposit(int amount);

        public abstract bool Withdraw(int amount);

        public abstract void Transfer(BankAccount to);
    }
}