using System.Collections.Generic;
using BanksLab.BankAccounts;

namespace BanksLab.Bank
{
    public class TinkoffBank : Bank
    {
        public TinkoffBank(int notValidateSum) : base(notValidateSum)
        {
        }
    }
}