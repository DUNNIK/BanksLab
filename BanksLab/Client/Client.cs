using System;

namespace BanksLab.Client
{
    public class Client
    {
        private string Name;
        private string Surname;
        private string PassportDetails;
        private string Address;
        public readonly string Id = Guid.NewGuid().ToString();

        public Client(string name, string surname, string address = null, string passportDetails = null)
        {
            Name = name;
            Surname = surname;
            Address = address;
            PassportDetails = passportDetails;
        }
        
        
    }
}