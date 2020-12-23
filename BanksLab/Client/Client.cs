using System.Collections.Generic;
using System.Linq;
using BanksLab.Bank;
using BanksLab.BankAccounts;
using BanksLab.Commands;
using BanksLab.Exceptions;

namespace BanksLab.Client
{
    public class Client
    {
        protected internal string Name;
        protected internal string Surname;
        protected internal string PassportDetails;
        protected internal string Address;
        public readonly List<string> BankAccountsIdsList;

        public Client()
        {
            BankAccountsIdsList = new List<string>();
        }
        protected Client(Client client)
        {
            Name = client.Name;
            Surname = client.Surname;
            Address = client.Address;
            PassportDetails = client.PassportDetails;
        }
        
        public static AllBanksManager ChooseBank => new AllBanksManager();

        public bool IsValidate()
        {
            return Name != null && Surname != null;
        }

        public void DepositMoneyOnYourBankAccount(string id, double amount)
        {
            if (IsItYourAccount(id)) throw new YourBankAccountException();
            var account = FindAccount(id);
            var command = new BankAccountCommand(account, BankAccountCommand.Action.Deposit, amount);
            account.UpdateAccountHistory(command);
            command.Call();
        }
        public void WithdrawMoneyOnYourBankAccount(string id, double amount)
        {
            if (IsItYourAccount(id)) throw new YourBankAccountException();
            var account = FindAccount(id);
            var command = new BankAccountCommand(account, BankAccountCommand.Action.Withdraw, amount);
            account.UpdateAccountHistory(command);
            command.Call();
        }
        public void TransferMoney(string fromId, string toId, double amount)
        {
            if (IsItYourAccount(fromId)) throw new YourBankAccountException();
            
            BankAccount fromAccount = FindAccount(fromId), toAccount = FindAccount(toId);

            var moneyTransferCommand = new MoneyTransferCommand(fromAccount, toAccount, amount);
            
            fromAccount.UpdateAccountHistory(moneyTransferCommand);
            toAccount.UpdateAccountHistory(moneyTransferCommand);
            
            moneyTransferCommand.Call();
        }
        private static BankAccount FindAccount(string id)
        {
            BankAccount account = null;
            foreach (var bank in AllBanksManager.Banks.Where(bank => bank.Accounts.ContainsKey(id)))
            {
                account = bank.Accounts[id];
            }

            return account;
        }

        public string BankAccountStatus(string id)
        {
            if (IsItYourAccount(id)) throw new YourBankAccountException();
            var account = FindAccount(id);
            return account.ToString();
        }
        private bool IsItYourAccount(string id)
        {
            return !BankAccountsIdsList.Contains(id);
        }

        public void RestorePreviousAccountState(string accountId)
        {
            if (IsItYourAccount(accountId)) throw new YourBankAccountException();
            var account = FindAccount(accountId);
            account.RestorePreviousAccountState();
        }
    }
}