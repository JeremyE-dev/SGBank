using System;
using System.IO;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using SGBank.Models.Interfaces;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        private string path = "C:/Users/Jeremy/source/repos/SGBank052020/SGBank.Data/Accounts.txt";

        private List<Account> AccountsList = new List<Account>();

        private Account _account;

        public FileAccountRepository()
        {
            ReadTheFile();

        }
        
        //this method will read the file and place each account in a list of acounts it in an list of accounts
        private void ReadTheFile()
        {
            if (File.Exists(path))
            {
                string[] rows = File.ReadAllLines(path);
                for (int i = 1; i < rows.Length; i++)
                {
                    string[] columns = rows[i].Split(',');
                    Account a = new Account();
                    a.AccountNumber = columns[0];
                    a.Name = columns[1];
                    a.Balance = Decimal.Parse(columns[2]);

                    switch (columns[3])
                    {
                        case "F":
                            a.Type = AccountType.Free;
                            break;
                        case "B":
                            a.Type = AccountType.Basic;
                            break;
                        case "P":
                            a.Type = AccountType.Premium;
                            break;
                        default:
                            Console.WriteLine("Account Type Not Supported or Null");
                            break;//maybe adapt this to return??

                    }

                    AccountsList.Add(a);

                }
            }

            else
            {
                Console.WriteLine("could not find file at {0}", path);

            }

        }

        private Account ExtractAccount(string AccountNumber)
        {
            Account extractedAccount = null;
           foreach(Account x in AccountsList)
            {
                if (x.AccountNumber == AccountNumber)
                    extractedAccount = x;
                
            }

            return extractedAccount;
         

        }
        
        //
        public Account LoadAccount(string AccountNumber)
        {
            _account = ExtractAccount(AccountNumber);
           
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
