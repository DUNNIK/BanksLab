using System;

namespace BanksLab.Exceptions
{
    public class YourBankAccountException : Exception
    {
        public YourBankAccountException() : base("You don't have such an account")
        {
        }
    }
}