using ATM_at.ATMModel;
using ATM_at.ATMController;
using ATM_at.ATMViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_at
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Controller controller = new Controller();
            controller.StartATM();
        }
    }
}
