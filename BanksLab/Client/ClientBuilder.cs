

namespace BanksLab.Client
{
    public class ClientBuilder
    {
        private readonly Client _client = new Client();
        public ClientBuilder AddName(string name)
        {
            _client.Name = name;
            return this;
        }

        public ClientBuilder AddSurname(string surname)
        {
            _client.Surname = surname;
            return this;
        }

        public ClientBuilder AddAddress(string address)
        {
            _client.Address = address;
            return this;
        }
        public ClientBuilder AddPassportDetails(string passportDetails)
        {
            _client.PassportDetails = passportDetails;
            return this;
        }
        public Client Build() => _client;
        public static implicit operator Client(ClientBuilder builder) => builder._client;
    }
}