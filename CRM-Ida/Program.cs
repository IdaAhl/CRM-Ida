using System;

namespace CRM_Ida
{
    class Program
    {
        static void Main(string[] args)
        {
            TestSql test = new TestSql();
       


            while (true)
            {
                Console.Clear();
                Console.Write(@"Kundregistret!

Vad vill du göra?
1) Skapa en ny kund
2) Ändra en kund
3) Ta bort en kund 
4) Hämta alla kunder
");
                var choise = Console.ReadLine();

                switch (choise)
                {
                    case "1":
                        Customer customer = new Customer();
                        var newCustomer = customer.MakeCustomer();
                        test.InstertCustomerToDatabase(newCustomer);
                        break;

                    case "2":
                        test.ReadName();
                        Console.Write("Vilken kund vill du ändra?");
                        var input = Int32.Parse(Console.ReadLine());

                        var newCustomerFromId = test.MakeCustomerFromId(input);
                        var changedCustomer = newCustomerFromId.ChangeCustomer(newCustomerFromId);
                        test.UpdateCustomerToDatabase(changedCustomer, input);
                        break;

                    case "3":
                        test.ReadName();
                        Console.Write("Vilken kund vill du ta bort?");
                        var inputC = Int32.Parse(Console.ReadLine());

                        test.DeleteCustomerFormDatabase(inputC);
                        break;

                    case "4":
                        Console.Clear();
                        test.ReadName();
                        break;
                    default:
                        break;

                }

            }



        }
    }
}
