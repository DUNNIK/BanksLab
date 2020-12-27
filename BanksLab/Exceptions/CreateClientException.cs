using System;

namespace BanksLab.Exceptions
{
    public class CreateClientException : Exception

    {
        public CreateClientException() : base("Creating a client is not possible!")
        {
        }
    }
}