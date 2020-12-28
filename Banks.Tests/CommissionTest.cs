using System;
using System.Threading;
using BanksLab;
using BanksLab.BankAccounts;
using BanksLab.Client;
using NUnit.Framework;

namespace Banks.Tests
{

    [TestFixture]
    public class CommissionTest
    {
        private Client _client1;

        [SetUp]
        public void Setup()
        {
            SystemTime.ResetDateTime();
            var clientBuilder1 = new ClientBuilder();
            _client1 = clientBuilder1
                .AddName("Huy")
                .AddSurname("Li")
                .AddAddress("R13")
                .AddPassportDetails("432342")
                .Build();
            _client1.ChooseBank
                .FirstBank()
                .CreateBankAccount
                .CreatCreditAccount(_client1, new CreditAccountInformation(-3000, 300, 5000));
        }

        [Test]
        public void CommissionCreditAccount()
        {
            SystemTime.SetDateTime(DateTime.Now.AddMonths(1).AddDays(2));
            _client1.WithdrawMoneyOnYourBankAccount(_client1.BankAccountsIdsList[0], 5100);
            Assert.That(_client1.BankAccountStatus(_client1.BankAccountsIdsList[0]), Is.EqualTo("Your Balance: -400"));
        }
    }
}