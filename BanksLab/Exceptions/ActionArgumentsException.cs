using System;

namespace BanksLab.Exceptions
{
    public class ActionArgumentsException : Exception
    {
        public ActionArgumentsException() : base("Invalid Arguments!")
        {
        }
    }
}