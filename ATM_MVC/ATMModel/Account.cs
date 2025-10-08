using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_at.ATMModel
{
    public class Account
    {
        public int id;
        public double balance;
        public Account(int id, double balance)
        {
            this.id = id;
            this.balance = balance;
        }
        public dynamic GetBalance()
        {
            return balance;
        }
        public dynamic Depoist(int number)
        {
            balance += number;
            return number;
        }
        public dynamic Withdraw(int number) {
            balance -= number;
            return number;
        }
        
    }
}
