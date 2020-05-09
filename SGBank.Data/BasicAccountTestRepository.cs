using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models.Interfaces;
using SGBank.Models;
namespace SGBank.Data
{
    class BasicAccountTestRepository:IAccountRepository
    {
        private static Account _account = new Account()
        {
            Name = "Basic Account",
            Balance = 100M,
            AccountNumber = "33333",
            Type = AccountType.Basic

        };

        public Account LoadAccount(string AccountNumber)
        {

            if (!AccountNumber.Equals(_account.AccountNumber))
                return null;
            else
            {
                return _account;
            }

        }

        public void SaveAccount(Account account)
        {
            _account = account;
        }

    }
}
