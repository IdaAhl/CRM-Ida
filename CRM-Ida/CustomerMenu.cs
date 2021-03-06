﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Ida
{
    class CustomerMenu
    {
        public void ShowCustomerMenu()
        {
            TestSql test = new TestSql();
            bool quitMenu = true;

            while (quitMenu)
            {
                Console.Clear();
                Console.Write(@"Kundregistret!
Vad vill du göra?
1) Skapa en ny kund
2) Ändra en kund
3) Lägg till fler telefonnummer
4) Ta bort en kund 
5) Hämta alla kunder
++) Gå tillbaka till huvudmenyn
");
                var choise = Console.ReadLine();

                switch (choise)
                {
                    case "1":
                        Customer customer = new Customer();
                        var newCustomer = customer.MakeCustomer();
                        var customerId = test.InstertCustomerToDatabase(newCustomer);
                        test.InsertCustomerPhoneNumberToDatabase(newCustomer, customerId);
                        break;

                    case "2":
                        test.ReadName();
                        Console.Write("Vilken kund vill du ändra?");
                        var input = Int32.Parse(Console.ReadLine());
                        var newCustomerFromId = test.MakeCustomerFromId(input);
                        var changedCustomer = newCustomerFromId.ChangeCustomer(newCustomerFromId);
                        test.UpdateCustomerToDatabase(changedCustomer, input);
                        test.DeletePhoneNumber(changedCustomer, input);
                        test.InsertCustomerPhoneNumberToDatabase(changedCustomer, input);
                        break;

                    case "3":
                        test.ReadName();
                        Console.Write("Till vilken kund vill du lägga till telefonnummer?");
                        var customerPhonenumber = Int32.Parse(Console.ReadLine());

                        List<PhoneNumber> phoneNumbers = new List<PhoneNumber>();

                        while (true)
                        {
                            PhoneNumber phoneNumber = new PhoneNumber();
                            var newPhoneNumber = phoneNumber.MakePhoneNumber();
                            phoneNumbers.Add(newPhoneNumber);

                            Console.WriteLine("Vill du lägga till ett nummer till Y/N");
                            if (Console.ReadLine() == "N")
                                break;
                        }
                        test.InsertCustomerPhoneNumberToDatabaseFromList(phoneNumbers, customerPhonenumber);

                        break;

                    case "4":
                        test.ReadName();
                        Console.Write("Vilken kund vill du ta bort?");
                        var inputC = Int32.Parse(Console.ReadLine());
                        test.DeleteCustomerFormDatabase(inputC);
                        break;

                    case "5":
                        Console.Clear();
                        test.ReadName();
                        break;
                    default:
                        quitMenu = false;
                        break;
                }

            }

        }
    }
}
