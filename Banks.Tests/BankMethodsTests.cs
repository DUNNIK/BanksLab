using BanksLab.BankAccounts;
using BanksLab.Client;
using NUnit.Framework;

namespace Banks.Tests
{
    [TestFixture]
    public class Tests
    {
        private Client _client;
        private Client _client1;
        
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
        public void TransferMoney_FromClientToClient1_Nothing()
        {
            _client.TransferMoney(_client.BankAccountsIdsList[0],_client1.BankAccountsIdsList[0], 300);
            Assert.That(_client.BankAccountStatus(_client.BankAccountsIdsList[0]), Is.EqualTo("Your Balance: 200"));
            Assert.That(_client1.BankAccountStatus(_client1.BankAccountsIdsList[0]), Is.EqualTo("Your Balance: 5000"));
        }
        
        [Test]
        public void TransferMoney_FromClientToClient1_50And450()
        {
            _client.TransferMoney(_client.BankAccountsIdsList[0],_client1.BankAccountsIdsList[0], 150);
            Assert.That(_client.BankAccountStatus(_client.BankAccountsIdsList[0]), Is.EqualTo("Your Balance: 50"));
            Assert.That(_client1.BankAccountStatus(_client1.BankAccountsIdsList[0]), Is.EqualTo("Your Balance: 5150"));
        }

        [Test]
        public void WithDrawMoney_Withdraw200FromClient1_4800()
        {
            _client1.WithdrawMoneyOnYourBankAccount(_client1.BankAccountsIdsList[0], 200);
            Assert.That(_client1.BankAccountStatus(_client1.BankAccountsIdsList[0]), Is.EqualTo("Your Balance: 4800"));
        }

        [Test]
        public void NotValidAccount_WithDraw700FromAccount_NothingNothing3800()
        {
            var clientBuilder2 = new ClientBuilder();

            var client2 = clientBuilder2
                .AddName("Nikita")
                .Build();
            client2.ChooseBank
                .FirstBank()
                .CreateBankAccount
                .CreatDebitAccount(client2, new DebitAccountInformation(4, 4000));
            client2.WithdrawMoneyOnYourBankAccount(client2.BankAccountsIdsList[0], 700);
            Assert.That(client2.BankAccountStatus(client2.BankAccountsIdsList[0]), Is.EqualTo("Your Balance: 4000"));

            clientBuilder2
                .AddSurname("Dunaev");
            client2.WithdrawMoneyOnYourBankAccount(client2.BankAccountsIdsList[0], 700);
            Assert.That(client2.BankAccountStatus(client2.BankAccountsIdsList[0]), Is.EqualTo("Your Balance: 4000"));
            client2.WithdrawMoneyOnYourBankAccount(client2.BankAccountsIdsList[0], 200);
            Assert.That(client2.BankAccountStatus(client2.BankAccountsIdsList[0]), Is.EqualTo("Your Balance: 3800"));
        }
        [Test]
        public void UnDo_TwoWithdraw500_5000()
        {
            _client1.WithdrawMoneyOnYourBankAccount(_client1.BankAccountsIdsList[0], 200);
            _client1.WithdrawMoneyOnYourBankAccount(_client1.BankAccountsIdsList[0], 200);
            _client1.TransferMoney(_client1.BankAccountsIdsList[0],_client.BankAccountsIdsList[0], 100);
            Assert.That(_client1.BankAccountStatus(_client1.BankAccountsIdsList[0]), Is.EqualTo("Your Balance: 4500"));
            _client1.RestorePreviousAccountState(_client1.BankAccountsIdsList[0]);
            _client1.RestorePreviousAccountState(_client1.BankAccountsIdsList[0]);
            _client1.RestorePreviousAccountState(_client1.BankAccountsIdsList[0]);
            Assert.That(_client1.BankAccountStatus(_client1.BankAccountsIdsList[0]), Is.EqualTo("Your Balance: 5000"));
            Assert.That(_client.BankAccountStatus(_client.BankAccountsIdsList[0]), Is.EqualTo("Your Balance: 200"));
        }
    }
}