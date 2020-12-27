using System.Collections.Generic;
using System.Linq;

namespace BanksLab.Bank
{
    public class AllBanksManager
    {
        public static readonly List<Bank> Banks = new List<Bank>{new Bank(500)};

        public Bank FirstBank()
        {
            return Banks[0];
        }
    }
}