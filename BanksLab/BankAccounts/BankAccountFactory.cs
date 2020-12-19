using System;

namespace BanksLab.BankAccounts
{
    public class BankAccountFactory : Bank.Bank
    {
        
        public BankAccountFactory CreatCreditAccount(Client.Client client, CreditAccountInformation creditAccountInformation)
        {
            var id = Guid.NewGuid().ToString();
            InformAccountIdToClient(client, id);
            Accounts.Add(
                id,
                new CreditAccount(client, NotValidateSum, creditAccountInformation)
                );
            return this;
        }
        public BankAccountFactory CreatDebitAccount(Client.Client client, DebitAccountInformation debitAccountInformation)
        {
            var id = Guid.NewGuid().ToString();
            InformAccountIdToClient(client, id);
            Accounts.Add(
                id, 
                new DebitAccount(client, NotValidateSum, debitAccountInformation)
                );
            return this;
        }
        public BankAccountFactory CreatDepositAccount(Client.Client client, DepositAccountInformation depositAccountInformation)
        {
            var id = Guid.NewGuid().ToString();
            InformAccountIdToClient(client, id);
            Accounts.Add(
                id, 
                new DepositAccount(client, NotValidateSum, depositAccountInformation)
                );
            return this;
        }

        private static void InformAccountIdToClient(Client.Client client, string id)
        {
            client.BankAccountsIdsList.Add(id);
        }

        public BankAccountFactory(Bank.Bank bank) : base(bank)
        {
        }
    }
}