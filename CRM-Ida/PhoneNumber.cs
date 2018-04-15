using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Ida
{
    public class PhoneNumber
    {
        public string Name { get; set; }

        public PhoneNumber MakePhoneNumber()
        {
            PhoneNumber newPhoneNumber = new PhoneNumber();
            Validate validate = new Validate();

            Console.Clear();
            Console.WriteLine("Lägg till ett telefonnummer");
            newPhoneNumber.Name = validate.InputFromUser(stringTypes.PhoneNumber);
            return newPhoneNumber;
        }
    }
}
