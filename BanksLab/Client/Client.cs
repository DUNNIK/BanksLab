using System;
using System.Collections.Generic;
using BanksLab.Bank;
using BanksLab.BankAccounts;

namespace BanksLab.Client
{
    public class Client
    {
        protected string Name;
        protected string Surname;
        private string PassportDetails;
        private string Address;
        public readonly string Id = Guid.NewGuid().ToString();
        public List<string> BankAccountsIdsList = new List<string>();

        public Client(string name, string surname, string address, string passportDetails)
        {
            Name = name;
            Surname = surname;
            Address = address;
            PassportDetails = passportDetails;
        }

        protected Client(Client client)
        {
            Name = client.Name;
            Surname = client.Surname;
            Address = client.Address;
            PassportDetails = client.PassportDetails;
        }
        
        public AllBanksManager ChooseBank => new AllBanksManager();

        public bool IsValidate()
        {
            return Name != null && Surname != null;
        }
    }
}