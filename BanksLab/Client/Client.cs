using System;
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
        private readonly string _name;
        private readonly string _surname;
        private readonly string _passportDetails;
        private readonly string _address;
        public readonly List<string> BankAccountsIdsList = new List<string>();

        public Client(string name, string surname, string passportDetails, string address)
        {
            _name = name;
            _surname = surname;
            _passportDetails = passportDetails;
            _address = address;
        }
        protected Client(Client client)
        {
            _name = client._name;
            _surname = client._surname;
            _address = client._address;
            _passportDetails = client._passportDetails;
        }
        
        public static AllBanksManager ChooseBank => new AllBanksManager();

        public bool IsNotDoubtful()
        {
            return _address != null && _passportDetails != null;
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

        public void GetMyPercents(string accountId, DateTime time)
        {
            
        }
    }
}