using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;

namespace SGBank.BLL.WithdrawRules
{
    class PremiumAccountWithdrawRule : IWithdraw
    {
        //rules: Must be negative, except for 
        public AccountWithdrawResponse Withdraw(Account account, decimal amount)
        {
            //need a response object to return

        }
    }
}
