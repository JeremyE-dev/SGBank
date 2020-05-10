﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
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
        [Test]
        [TestCase("33333", "Basic Account", 1500, AccountType.Basic, -1000, 1500, false)]//fail, too much withdrawn
        [TestCase("33333", "Basic Account", 100, AccountType.Free, -100, 100, false)]//not a basic account type
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, 100, 100, false)]//fail, positive number withdrawn
        [TestCase("33333", "Basic Account", 150, AccountType.Basic, -50, 100, true)]//success
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, -150, -60, true)]//fail, too much withdrawn

        public void BasicAccountWithdrawRuleTest(string accountNumber, string name, decimal balance,
          AccountType accountType, decimal amount, decimal newBalance,  bool expectedResult)
        {
            IWithdraw withdraw = new BasicAccountWithdrawRule();

            Account account = new Account()
            {
                Name = name,
                AccountNumber = accountNumber,
                Balance = balance,
                Type = accountType

            };

            AccountWithdrawResponse response = withdraw.Withdraw(account, amount);

            Assert.AreEqual(expectedResult, response.Success);
            if (response.Success)
            {
                Assert.AreEqual(newBalance,response.Account.Balance);
            }




        }
    }
}
