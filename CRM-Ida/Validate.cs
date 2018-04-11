﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace CRM_Ida
{
    public class Validate
    {
        public string ValidateInput(stringTypes stringTypes)
        {
            string fromUser;
            while (true)
            {
                fromUser = AskForInput(stringTypes);
                if (ValidateWhithType(stringTypes, fromUser))
                    break;
                else
                {
                    Console.WriteLine("Något gick fel, försök igen");
                    continue;
                }
            }
            return fromUser;
        }

        public string AskForInput(stringTypes stringTypes)
        {
            switch (stringTypes)
            {
                case stringTypes.FirstName:
                    Console.Write("Förnamn: ");
                    break;
                case stringTypes.LastName:
                    Console.Write("Efternamn: ");
                    break;
                case stringTypes.Epost:
                    Console.Write("E-post: ");
                    break;
                case stringTypes.PhoneNumber:
                    Console.Write("Telefonnummer: ");
                    break;
                default:
                    Console.WriteLine();
                    break;
            }
            return Console.ReadLine();
        }

        public bool ValidateWhithType(stringTypes stringType, string stringToValidate)
        {
            var isValid = false;

            switch (stringType)
            {
                case stringTypes.FirstName:
                case stringTypes.LastName:
                    // isValid = Validate(ValidateWord, stringToValidate);
                    isValid = ValidateWord(stringToValidate);
                    break;
                case stringTypes.Epost:
                    isValid = ValidateEmail(stringToValidate);
                    // isValid = Validate(ValidateEmail, stringToValidate);
                    break;
                case stringTypes.PhoneNumber:
                    //isValid = Validate(ValidatePhoneNo, stringToValidate);
                    //isValid = ValidatePhoneNo(stringToValidate);
                    isValid = true;
                    break;
                default:
                    Console.WriteLine();
                    break;
            }
            return isValid;
        }


        /* En fin metod
        public bool Validate(Func<string, bool> validate, string stringToValidate)
        {
            bool answer = validate(stringToValidate);
            return answer;
        }
        */

        public bool ValidatePhoneNo(string input)
        {
            return Regex.IsMatch(input, @"^\d{2,10}-?\d{1,8}$");
        }

        public bool ValidateEmail(string input)
        {
            return Regex.IsMatch(input, @"^\w+(\.\w+)*@\w+(\.\w+)*\.\w{2,10}$");
        }

        public bool ValidateWord(string input)
        {
            return Regex.IsMatch(input, @"^[a-zA-ZåäöÅÄÖ]+$");
        }

    }
}
