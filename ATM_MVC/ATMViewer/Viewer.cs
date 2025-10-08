using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using ATM_at.ATMModel;

namespace ATM_at.ATMViewer
{
    internal class Viewer
    {
        public dynamic InitalStage(int id)
        {
            
            Console.WriteLine("Pick a operation 1.Check Balance 2.Deposit 3.Withdraw 4.Exit");
            string choice = Console.ReadLine();
            string pattern = "[1-4]";
            while (!(Regex.IsMatch(choice, pattern)))
            {
                Console.WriteLine("Pick a operation 1.Check Balance 2.Deposit 3.Withdraw 4.Exit");
                choice = Console.ReadLine();
            }
            return choice;
        }
        public void Deposit(int id, Account account)
        {
            Console.WriteLine("Enter how much you would like to deposit");
           
            try
            {
                int number = int.Parse(Console.ReadLine());
                while (number <= 0)
                {
                    Console.WriteLine("Enter another number");
                    number = int.Parse(Console.ReadLine());
                }
                Console.WriteLine($"Successfuly deposited ${account.Depoist(number)} into bank account");
            }
            catch {
                Console.WriteLine("Try again");
                int number = int.Parse(Console.ReadLine());
                while (number <= 0)
                {
                    Console.WriteLine("Enter another number");
                    number = int.Parse(Console.ReadLine());
                }
                Console.WriteLine($"Successfuly deposited ${account.Depoist(number)} into bank account");
            }
            
        }
        public void Withdraw(int id, Account account) {
            Console.WriteLine("Enter how much you would like to withdraw");
            try
            {
                int number = int.Parse(Console.ReadLine());
                while ((number <= 0 || account.balance < number))
                {
                    Console.WriteLine("Try again.");
                    number = int.Parse(Console.ReadLine());
                }
                Console.WriteLine($"Successfuly withdrew ${account.Withdraw(number)} from bank account");
            }
            catch
            {
                Console.WriteLine("Try again.");
                int number = int.Parse(Console.ReadLine());
                while ((number <= 0 || account.balance < number))
                {
                    number = int.Parse(Console.ReadLine());
                }
                Console.WriteLine($"Successfuly withdrew ${account.Withdraw(number)} from bank account");

            }
        }
        public dynamic GetId() {
            Console.WriteLine("Enter ID");

            try
            {
                int id = int.Parse(Console.ReadLine());
                return id;
            }
            catch {
                Console.WriteLine("Try again");
                int id = int.Parse(Console.ReadLine());
                return id;

            }
            
        }
        public void GetBalance(int id, Account account)
        {
            Console.WriteLine(account.GetBalance());
        }
    }
}
