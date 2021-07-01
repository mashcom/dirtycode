using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace StandardChartered
{
    class Validator
    {
        public static String titleValidation(String title)
        {
            String[] validTitle ={ "Mr", "Mrs", "Ms", "Dr", "Prof", "Other" } ;
            return (validTitle.Contains(title))?"":"Invalid input type any of the following options Mr, Mrs, Ms, Dr, Prof, Other";
        }

        public static String residenceValidation(String input)
        {
            String[] validInput = { "No", "Yes"};
            return (validInput.Contains(input)) ? "" : "Invalid input type any of the following options 'Yes' or 'No'";
        }

        public static String NameValidation(String input,Boolean required=true)
        {
            return "";
            //if (input.Length == 0 && required == false) return "";
            //return (!Regex.Match(input, "^[A-Z][a-zA-Z]*$").Success) ? "" : "Please enter a valid name";
        }

        public static String DateOfBirthValidation(String input, Boolean required = true)
        {
            Match dobMatch = Regex.Match(input, @"^(?:(?:(?:0?[13578]|1[02])(\/|-|\.)31)\1|(?:(?:0?[1,3-9]|1[0-2])(\/|-|\.)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:0?2(\/|-|\.)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:(?:0?[1-9])|(?:1[0-2]))(\/|-|\.)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$");

            return (!dobMatch.Success) ? "Date of Birth input format invalid" : "";
        }

        public static String idTypeValidation(String title)
        {
            String[] validTitle = { "1", "2", "3"};
            return (validTitle.Contains(title)) ? "" : "Invalid input type any of the following options 1. Passport  2. National ID  3. Driver's Licence";
        }

    }
}
