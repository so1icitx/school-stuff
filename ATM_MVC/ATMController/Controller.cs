using ATM_at.ATMModel;
using ATM_at.ATMViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_at.ATMController
{
    internal class Controller
    {
       
        public void StartATM()
        {
            
            bool shouldRun = true;
            Viewer viewer = new Viewer();
            int ID = viewer.GetId();
            Account account = new Account(ID, 0.0);
            
            while (shouldRun)
            {
                
                string choice = viewer.InitalStage(ID);
                switch (choice)
                {
                    case "1":
                        viewer.GetBalance(ID, account);
                        break;
                    case "2":
                        viewer.Deposit(ID, account);       
                         break;
                    case "3":
                        viewer.Withdraw(ID, account);
                        break;
                    case "4":
                        shouldRun = false;
                        break;
                    default:
                        break;

                }
            }

        }
    }
}
