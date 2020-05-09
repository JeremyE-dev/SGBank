using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.BLL.DepositRules;
using SGBank.Models.Responses;

namespace SGBank.Tests
{
    [TestFixture]
    public class BasicAccountTests
    {
        [Test]
        [TestCase("33333", "Basic Account", 100, AccountType.Free, 250, false)]//fail wrong account type
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, -100, false)]//fail negative number deposited
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, 250, true)]//success


        public void BasicAccountDepositRuleTest(string accountNumber, string name, decimal balance,
          AccountType accountType, decimal amount, bool expectedResult)
        {
            IDeposit deposit = new NoLimitDepositRule();
            Account account = new Account();
            account.Name = name;
            account.AccountNumber = accountNumber;
            account.Balance = balance;
            account.Type = accountType;

            AccountDepositResponse response = deposit.Deposit(account, amount);

            Assert.AreEqual(expectedResult, response.Success);

        }
    }
}
