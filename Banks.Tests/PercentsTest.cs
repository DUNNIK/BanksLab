using System;
using System.Threading;
using BanksLab;
using BanksLab.BankAccounts;
using BanksLab.Client;
using NUnit.Framework;

namespace Banks.Tests
{
    [TestFixture]
    public class PercentsTest
    {
        private Client _client;
        
        [SetUp]
        public void Setup()
        {
            var clientBuilder = new ClientBuilder();

            _client = clientBuilder
                .AddName("Nikita")
                .AddSurname("Dunaev")
                .AddAddress("ABC123")
                .AddPassportDetails("727272")
                .Build();
            _client.ChooseBank
                .FirstBank()
                .CreateBankAccount
                .CreatDebitAccount(_client, new DebitAccountInformation(365, 200));
            SystemTime.SetDateTime(DateTime.Now.AddMonths(1).AddDays(2));
        }
        
        [Test]
        public void PercentsDebitAccount_OneMonth_OneTimePercents()
        {
            SystemTime.SetDateTime(DateTime.Now.AddMonths(1).AddDays(2));
            Thread.Sleep(2000);
            Assert.That(_client.BankAccountStatus(_client.BankAccountsIdsList[0]), Is.EqualTo("Your Balance: 6400"));
        }
    }
}