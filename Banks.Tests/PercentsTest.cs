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
            SystemTime.ResetDateTime();
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
                .CreatDebitAccount(_client, new DebitAccountInformation(12, 200));
        }
        
        [Test]
        public void PercentsDebitAccount_OneMonth_OneTimePercents()
        {
            SystemTime.SetDateTime(DateTime.Now.AddMonths(1).AddDays(2));
            _client.DepositMoneyOnYourBankAccount(_client.BankAccountsIdsList[0], 0);
            Assert.That(_client.BankAccountStatus(_client.BankAccountsIdsList[0]), Is.EqualTo("Your Balance: 202"));
        }
    }
}