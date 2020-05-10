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
        private string path = "C:/Users/Jeremy/source/repos/SGBank052020/SGBank.Data/Account.txt";

        private List<Account> AccountsList = new List<Account>();
        
        //this method will read the file and place each account in a list of acounts it in an list of accounts
        public void ReadTheFile()
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
        
        //not that I have several accounts in a list which one do I load??
        public Account LoadAccount(string AccountNumber)
        {
            //throw new NotImplementedException();

            //look in the accountList and if it exists return it
            //if it does not exist return null
            //assume there will not be more than one of the same account number
            foreach(Account a in AccountsList) //could this be a linq query??
            {
                if(a.AccountNumber == AccountNumber)
                {
                    return a;
                }

               
            }

            return null;

        }

        public void SaveAccount(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
