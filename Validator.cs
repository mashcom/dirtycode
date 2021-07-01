using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace StandardChartered {
    class Validator {
        public static String titleValidation(String title) {
            String[] validTitle = { "MR", "MR", "MS", "DR", "PROF", "OTHER" };
            return (validTitle.Contains(title.ToUpper())) ? "" : "Invalid input type any of the following options Mr, Mrs, Ms, Dr, Prof, Other";
        }

        public static String residenceValidation(String input) {
            String[] validInput = { "NO", "YES" };
            return (validInput.Contains(input.ToUpper())) ? "" : "Invalid input type any of the following options 'Yes' or 'No'";
        }

        public static String NameValidation(String input, Boolean required = true) {
            if (input.Length == 0 && required == false) return "";
            return (Regex.Match(input, "^[A-Z][a-zA-Z]*$").Success) ? "" : "Please enter a valid name";
        }

        public static String dateValidation(String input, Boolean required = true) {
            //return "";
            Match dobMatch = Regex.Match(input, @"^(?:(?:(?:0?[13578]|1[02])(\/|-|\.)31)\1|(?:(?:0?[1,3-9]|1[0-2])(\/|-|\.)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:0?2(\/|-|\.)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:(?:0?[1-9])|(?:1[0-2]))(\/|-|\.)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$");
            return (dobMatch.Success) ? "" : "Date of Birth input format invalid: Correct Format MM/DD/YYYY";
        }

        public static String idTypeValidation(String title) {
            String[] validTitle = { "1", "2", "3" };
            return (validTitle.Contains(title)) ? "" : "Invalid input type any of the following options 1. Passport  2. National ID  3. Driver's Licence";
        }

        public static int wholeNumberValidation(String input, bool required = true) {
            if (input.Length == 0 && required == true) throw new FormatException("Input is required");
            if (input.Length == 0) return 0;
            try {
                int parsedNumber = int.Parse(input.ToString());
                if (parsedNumber < 0) throw new FormatException("Number should be above or equal to 0");
                return parsedNumber;
            } catch (Exception) {
                throw new FormatException("Input should be a whole number above above or equal to 0");
            }

        }
        public static long phoneNumberValidation(String input, bool required = true) {
            input = input.Replace(" ","");
            if (input.Length < 5 && required == true) throw new FormatException("Input is too short, enter at least 5 digits");
            if (input.Length == 0) return 0;
            if(input.Length>14) throw new FormatException("Input is too long, maximum allowed is 14 digits");

            try {
                long parsedNumber = long.Parse(input.ToString());
                if (parsedNumber < 0) throw new FormatException("Number should be above or equal to 0");
                return parsedNumber;
            } catch (Exception) {
                throw new FormatException("Input should contain numbers only");
            }

        }
    }
}
