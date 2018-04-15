using System;
using System.Collections.Generic;
using System.Text;
using CRM_Ida;

namespace CRM_Ida
{
    class App
    {
        readonly CustomerMenu _customerMenu = new CustomerMenu();

        public void Run()
        {


            while (true)
            {
                Console.Clear();
                Console.Write(@"IddDayDreamSystemAdvanceCareCustomerSupport

Vad vill du göra? 
1) Titta till kunderna?
2) Ändra i produkter?");
                var choise = Console.ReadLine();
                switch (choise)
                {
                    case "1":
                        _customerMenu.ShowCustomerMenu();
                        break;
                    case "2":
                        var productMenu = new PruductMenu();
                        productMenu.ShowProductMenu();
                        break;
                    default:
                        break;
                }


            }

        }
    }
}
