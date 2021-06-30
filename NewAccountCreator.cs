using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardChartered
{
    class NewAccountCreator
    {

        public Dictionary<string, string> accountDetails = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            var newAccount = new NewAccountCreator();
           
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("Welcome to Standard Chartered Bank");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("");

            
            
            Console.WriteLine("1. PLEASE TELL US ABOUT YOURSELF?");

            //get title and validate the input
            EnterTitle:
            Console.Write("Enter Title:");
            String title = Console.ReadLine();
            String titleError = Validator.titleValidation(title);
            if (titleError != "")
            {
                Console.WriteLine(titleError);
                goto EnterTitle;
            }
            
            newAccount.accountDetails.Add("title",title);

            //get firstname and validate the input
            EnterFirstName:
            Console.Write("Enter First Name:");
            String firstName = Console.ReadLine();
            String firstNameError = Validator.NameValidation(firstName);
            if (firstNameError != "") {
                Console.WriteLine(firstNameError);
                goto EnterFirstName;
            }
            newAccount.accountDetails.Add("firstName", firstName);

             //get firstname and validate the input
             EnterMiddleName:
            Console.Write("Enter Middle Name (Optional):");
            String middleName = Console.ReadLine();
            String middleNameError = Validator.NameValidation(middleName);
            if (middleNameError != "")
            {
                Console.WriteLine(middleNameError);
                goto EnterMiddleName;
            }
            newAccount.accountDetails.Add("middleName", middleName);

            //get last and validate the input
            EnterLastName:
            Console.Write("Enter Last Name:");
            String lastName = Console.ReadLine();
            String lastNameError = Validator.NameValidation(lastName);
            if (lastNameError != "")
            {
                Console.WriteLine(lastNameError);
                goto EnterLastName;
            }
            newAccount.accountDetails.Add("lastName", lastName);


            //get residence status and validate
            EnterResidence:
            Console.Write("Are you a resident of Zimbabwe? ");
            String residence = Console.ReadLine();
            String residenceError = Validator.NameValidation(residence);
            if (residenceError != "")
            {
                Console.WriteLine(residenceError);
                goto EnterResidence;
            }
            newAccount.accountDetails.Add("residence", residence);

            //request passport number if resident of Zimbabwe
            if (residence == "No")
            {
                EnterPassportNumber:
                Console.Write("Enter passport number");
                String passportNumber = Console.ReadLine();
                if(passportNumber == "")
                {
                    Console.WriteLine("Please Enter Passport Number");
                    goto EnterPassportNumber;
                }
                newAccount.accountDetails.Add("passportNumber", passportNumber);
            }

             //get dob and validate the input
            EnterDateOfBirth:
            Console.Write("Enter Date of Birth Name:");
            String dateOfBirth = Console.ReadLine();
            String dateOfBirthError = Validator.NameValidation(dateOfBirth);
            if (dateOfBirthError != "")
            {
                Console.WriteLine(dateOfBirthError);
                goto EnterDateOfBirth;
            }
            newAccount.accountDetails.Add("dateOfBirth", dateOfBirth);

            Console.Write(newAccount.accountDetails.ToString());
            Console.ReadLine();


        }
    }
}
