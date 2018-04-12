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
        public List<Product> FavoriteProducts { get; set; }

        public Customer()
        {
            PhoneNumber = new List<string>();
            FavoriteProducts = new List<Product>();
        }

        public Customer MakeCustomer()
        {
            Customer newCustomer = new Customer();
            Validate validate = new Validate();

            Console.Clear();
            Console.WriteLine("Skapa en ny kund");

            newCustomer.FirstName = validate.InputFromUser(stringTypes.FirstName);
            newCustomer.LastName = validate.InputFromUser(stringTypes.LastName);
            newCustomer.Epost = validate.InputFromUser(stringTypes.Epost);
            newCustomer.PhoneNumber.Add(validate.InputFromUser(stringTypes.PhoneNumber));
            return newCustomer;
        }

        public Customer ChangeCustomer(Customer customer)
        {
            Console.Write(@"Vad vill du ändra? 1 Förnamn, 2 Efternamn, 3 Epost, 4 Telefonnummer?");

            var inputChange = Console.ReadLine();
            Validate validate = new Validate();

            switch (inputChange)
            {
                case "1":
                    customer.FirstName = validate.InputFromUser(stringTypes.FirstName);
                    break;
                case "2":
                    customer.LastName = validate.InputFromUser(stringTypes.LastName);
                    break;
                case "3":
                    customer.Epost = validate.InputFromUser(stringTypes.Epost);
                    break;
                case "4":
                    customer.ChangePhoneNumber(customer);
                    break;
            }
            return customer;
        }

        public void ChangePhoneNumber(Customer customer)
        {
            Validate validate = new Validate();

            if (customer.PhoneNumber.Count == 0)
                customer.PhoneNumber.Add(validate.InputFromUser(stringTypes.PhoneNumber));
            else
            {
                for (int i = 0; i < customer.PhoneNumber.Count; i++)
                {
                    Console.WriteLine($"Vilket telefonnummer vill du ändra {i + 1}: {customer.PhoneNumber[i]}");
                }
                var numberToChange = Convert.ToInt32(Console.ReadLine());
                customer.PhoneNumber[numberToChange - 1] = validate.InputFromUser(stringTypes.PhoneNumber);
            }

            

        }

    }
}