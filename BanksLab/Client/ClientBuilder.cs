

using BanksLab.Exceptions;

namespace BanksLab.Client
{
    public class ClientBuilder
    {
        private string _name;
        private string _surname;
        private string _address;
        private string _passportDetails;
        public ClientBuilder AddName(string name)
        {
            _name = name;
            return this;
        }

        public ClientBuilder AddSurname(string surname)
        {
            _surname = surname;
            return this;
        }

        public ClientBuilder AddAddress(string address)
        {
            _address = address;
            return this;
        }
        public ClientBuilder AddPassportDetails(string passportDetails)
        {
            _passportDetails = passportDetails;
            return this;
        }
        public Client Build()
        {
            if (IsNotValidate()) throw new CreateClientException();
            return new Client(_name, _surname, _passportDetails, _address);
        }

        private bool IsNotValidate()
        {
            return _name == null && _surname == null;
        }
    }
}