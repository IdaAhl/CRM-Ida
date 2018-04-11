using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace CRM_Ida
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Epost { get; set; }
        public List<string> PhoneNumber { get; set; }

        public Customer()
        {
            PhoneNumber = new List<string>();
        }

        public Customer MakeCustomer()
        {
            Customer newCustomer = new Customer();
            Validate help = new Validate();

            Console.Clear();
            Console.WriteLine("Skapa en ny kund");

            newCustomer.FirstName = help.ValidateInput(stringTypes.FirstName);
            newCustomer.LastName = help.ValidateInput(stringTypes.LastName);
            newCustomer.Epost = help.ValidateInput(stringTypes.Epost);


            newCustomer.PhoneNumber.Add(help.ValidateInput(stringTypes.PhoneNumber));
            return newCustomer;
        }

        public Customer ChangeCustomer(Customer customer)
        {
            Console.Write(@"Vad vill du ändra?
1 Förnamn
2 Efternamn
3 Epost
4 telefonnummer?");

            var inputChange = Console.ReadLine();
            Validate help = new Validate();

            switch (inputChange)
            {
                case "1":
                    customer.FirstName = help.ValidateInput(stringTypes.FirstName);
                    break;
                case "2":
                    customer.LastName = help.ValidateInput(stringTypes.LastName);
                    break;
                case "3":
                    customer.Epost = help.ValidateInput(stringTypes.Epost);
                    break;
                case "4":
                    //customer.PhoneNumber = help.ValidateInput(stringTypes.PhoneNumber);
                    break;
            }
            return customer;
        }

    }
}