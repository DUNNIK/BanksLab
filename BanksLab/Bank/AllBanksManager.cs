using System.Collections.Generic;
using System.Linq;

namespace BanksLab.Bank
{
    public class AllBanksManager
    {
        private static List<Bank> Banks = new List<Bank>{new TinkoffBank(500)};

        public Bank Tinkoff()
        {
            foreach (var bank in Banks.OfType<TinkoffBank>())
            {
                return bank;
            }

            return new TinkoffBank(1000);
        }
    }
}