using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StandardChartered
{
    class NewAccountCreator
    {

        public Dictionary<string, string> accountDetails = new Dictionary<string, string>();
        static string activeAccount;

        static void Main(string[] args)
        {
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("Welcome to Standard Chartered Bank");
            Console.WriteLine("-----------------------------------------------------");
            Start:
            Console.WriteLine("Please one option below");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Balance Enquiry");
            Console.WriteLine("3. Deposit Funds");
            Console.WriteLine("4. Withdraw Funds");

            switch (Console.ReadLine())
            {
                case "1":
                    createAccount();
                break;
                case "2":
                    balanceEnquiry();
                break;
                case "3":
                    changeBalance("deposit");
                break;
                case "4":
                    changeBalance("withdraw");
                    break;
                default:
                    Console.WriteLine("Invalid Option");
                    goto Start;
                    break;
            }
            goto Start;

        }

        static string generateAccountNumber()
        {
            Random accountNumberGenerator = new Random();
            int accountNumberStart = accountNumberGenerator.Next(123456);
            int accountNumberEnd = accountNumberGenerator.Next(1234556);
            String uniqueAccountNumber = accountNumberStart.ToString() + accountNumberEnd.ToString();
            
            return uniqueAccountNumber;
        }


        static void changeBalance(String action)
        {

            StartDepositProcess:
            Console.WriteLine("Please enter the 11 digit account number:");
            String accountNumber = Console.ReadLine();
            activeAccount = accountNumber;
            float balance;
            try
            {
                 balance = getStoredBalance(activeAccount);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Account not found!");
                goto StartDepositProcess;
            }

            EnterDepositAmount:
            Console.WriteLine("Please enter the amount you wish to "+action);
            String depositAmount = Console.ReadLine();
            float depositParsed;
            try
            {
                depositParsed = float.Parse(depositAmount);

                if(depositParsed <= 0)
                {
                    Console.WriteLine("Invalid input, amount should be greater than 0!");
                    goto EnterDepositAmount;
                }
                if (action == "withdraw")
                {
                    depositParsed = depositParsed * -1;
                }
                StreamWriter streamWriterForBalance = new StreamWriter("C:\\laragon\\www\\standard\\" + activeAccount + "_balance.txt");
                float newBalance = balance + depositParsed;
                streamWriterForBalance.Write(newBalance);
                streamWriterForBalance.Close();

                Console.WriteLine("Your new balance:$"+ newBalance );
                Console.WriteLine("Press any key to continue:");
                Console.ReadLine();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid amount format, please try again!");
                goto EnterDepositAmount;
            }
            Console.WriteLine(balance.ToString());
            

        }

        static float getStoredBalance(String accountNumber)
        {
            StreamReader reader = new StreamReader("C:\\laragon\\www\\standard\\" + activeAccount + "_balance.txt");
            string balance="";
            while (reader.EndOfStream == false) { 
                balance = reader.ReadLine(); 
            }
            reader.Close();
            return float.Parse(balance);
        }

        static void balanceEnquiry()
        {
        StartEnquiryProcess:
            Console.WriteLine("Please enter the 11 digit account number:");
            String accountNumber = Console.ReadLine();
            activeAccount = accountNumber;
            float balance;
            try
            {
                balance = getStoredBalance(activeAccount);
                Console.WriteLine("Your current balance is $"+balance.ToString());
                Console.WriteLine("Press any key to continue:");
                Console.ReadLine();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Account not found!");
                goto StartEnquiryProcess;
            }
        }

        static void createAccount()
        {

            String accountNumber = generateAccountNumber();
            
            //create files for account details and account balance
            StreamWriter streamWriter = new StreamWriter("C:\\laragon\\www\\standard\\" + accountNumber + ".txt");
            StreamWriter streamWriterForBalance = new StreamWriter("C:\\laragon\\www\\standard\\" + accountNumber + "_balance.txt");

            streamWriterForBalance.WriteLine(0);
            streamWriterForBalance.Close();

            var newAccount = new NewAccountCreator();
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

           
            streamWriter.WriteLine("title: "+ title);

        //get firstname and validate the input
        EnterFirstName:
            Console.Write("Enter First Name:");
            String firstName = Console.ReadLine();
            String firstNameError = Validator.NameValidation(firstName);
            if (firstNameError != "")
            {
                Console.WriteLine(firstNameError);
                goto EnterFirstName;
            }
            
            streamWriter.WriteLine("firstName: "+ firstName);

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
            streamWriter.WriteLine("middleName: "+ middleName);

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
            streamWriter.WriteLine("lastName: " + lastName);


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
            streamWriter.WriteLine("residence: " + residence);

            //request passport number if resident of Zimbabwe
            if (residence == "No")
            {
            EnterPassportNumber:
                Console.Write("Enter passport number");
                String passportNumber = Console.ReadLine();
                if (passportNumber == "")
                {
                    Console.WriteLine("Please Enter Passport Number");
                    goto EnterPassportNumber;
                }
                streamWriter.WriteLine("passportNumber: " + passportNumber);
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
            streamWriter.WriteLine("dateOfBirth: "+ dateOfBirth);

        EnterIdType:
            Console.Write("Enter ID Type:");
            Console.WriteLine("1. Passport  2. National ID  3. Driver's Licence");
            String idType = Console.ReadLine();
            String idTypeError = Validator.idTypeValidation(idType);
            if (idTypeError != "")
            {
                Console.WriteLine(idTypeError);
                goto EnterIdType;
            }
            streamWriter.WriteLine("idType: " + idType);


            //add  issue date date
            Console.Write("Enter Issue/Expiry Date:");
            String issueOrExpiryDate = Console.ReadLine();
            streamWriter.WriteLine("issueOrExpiryDate: " + issueOrExpiryDate);


            //add  issuer country
            Console.Write("Enter Issuer Country:");
            String issuerCountry = Console.ReadLine();
            streamWriter.WriteLine("issuerCountry: " + issuerCountry);

            //add  nationality country
            Console.Write("Enter Nationality:");
            String nationality = Console.ReadLine();
            streamWriter.WriteLine("nationality: " + nationality);

            //add  citizenship country
            Console.Write("Enter Citizenship:");
            String citizenship = Console.ReadLine();
            streamWriter.WriteLine("citizenship: " + citizenship);

        //add  gender country
        EnterGender:
            Console.Write("Enter Gender:");
            String gender = Console.ReadLine();
            if (gender != "Male" && gender != "Female")
            {
                Console.WriteLine("Gender should be Male or Female");
                goto EnterGender;
            }
            streamWriter.WriteLine("gender: " + gender);


        //add  marital status
        EnterMaritalStatus:
            Console.Write("Enter Enter Marital Status:");
            String maritalStatus = Console.ReadLine();
            if (maritalStatus == "")
            {
                Console.WriteLine("Marital Status is required");
                goto EnterMaritalStatus;
            }
            streamWriter.WriteLine("maritalStatus: " + maritalStatus);

            //spouse name
            Console.Write("Enter Enter Spouse Name (Optional):");
            String spouseName = Console.ReadLine();
            streamWriter.WriteLine("spouseName: " + spouseName);

            //number of children
            Console.Write("Enter Number of Children (Optional):");
            String numberOfChildren = Console.ReadLine();
            streamWriter.WriteLine("numberOfChildren: " + numberOfChildren);

            //number of dependents
            Console.Write("Enter Number of Dependents (Optional):");
            String numberOfDependents = Console.ReadLine();
            streamWriter.WriteLine("numberOfDependents: " + numberOfDependents);

            //education
            Console.Write("Enter Education:");
            String education = Console.ReadLine();
            streamWriter.WriteLine("education: " + education);

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("1A. CONTACT DETAILS?");
            Console.WriteLine("-------------------------------------------------");

            //country code
            Console.Write("Enter Country Code:");
            String countryCode = Console.ReadLine();
            streamWriter.WriteLine("countryCode: " + countryCode);

            //mobile
            Console.Write("Enter Mobile Number:");
            String mobileNumber = Console.ReadLine();
            streamWriter.WriteLine("mobileNumber: " + mobileNumber);

            //email
            Console.Write("Enter Email Address:");
            String emailAddress = Console.ReadLine();
            streamWriter.WriteLine("emailAddress: " + emailAddress);

            //email
            Console.Write("Enter Mailing Address:");
            String mailingAddress = Console.ReadLine();
            streamWriter.WriteLine("mailingAddress: " + mailingAddress);

            //ownership  residence
            Console.Write("Enter Ownership of Residence:");
            String onwnershipOfResidence = Console.ReadLine();
            streamWriter.WriteLine("onwnershipOfResidence: " + onwnershipOfResidence);

            //rental amount
            Console.Write("If rented, Rental Amount:");
            String rentalAmount = Console.ReadLine();
            streamWriter.WriteLine("rentalAmount: " + rentalAmount);

            //Physical Residential Address
            Console.Write("Enter Physical Residential Address");
            String physicalAddress = Console.ReadLine();
            streamWriter.WriteLine("physicalAddress: " + physicalAddress);

        //number of years
        EnterYearsAtResidence:
            Console.Write("Enter No. of Years Stayed :");
            float yearsAtResidence;
            try
            {
                yearsAtResidence = float.Parse(Console.ReadLine());
            }catch(Exception e)
            {
                Console.WriteLine("Invalid value for input Years At Residence");
                goto EnterYearsAtResidence;
            }
            
            streamWriter.WriteLine("yearsAtResidence: " + yearsAtResidence.ToString());

            if (yearsAtResidence < 3)
            {
                //Previous Residential Address
                Console.Write("Enter Previous Physical Residential Address");
                String previousAddress = Console.ReadLine();
                streamWriter.WriteLine("previousAddress: " + previousAddress);
            }

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("1B. SOURCE OF FUNDS?");
            Console.WriteLine("-------------------------------------------------");

            //Nature Of Employment
            Console.Write("Enter Nature of Employment: e.g Salaried, Self-employed or any other");
            String natureOfEmployment = Console.ReadLine();
            streamWriter.WriteLine("natureOfEmployment: " + natureOfEmployment);

            //Nature Of Employment
            Console.Write("Enter Employment Terms: e.g Permanent, Contract or any other");
            String employmentTerms = Console.ReadLine();
            streamWriter.WriteLine("employmentTerms: " + employmentTerms);

            //Name of Business
            Console.Write("Enter Name of Employer/Business");
            String business = Console.ReadLine();
            streamWriter.WriteLine("business: " + business);

            //Nature of Business
            Console.Write("Enter Name of Employer/Business");
            String natureOfbusiness = Console.ReadLine();
            streamWriter.WriteLine("natureOfbusiness: " + natureOfbusiness);

            //Employers Address
            Console.Write("Enter Employers Address");
            String employerAddress = Console.ReadLine();
            streamWriter.WriteLine("employerAddress: " + employerAddress);

            //Qualification
            Console.Write("Enter Qualification");
            String qualification = Console.ReadLine();
            streamWriter.WriteLine("qualification: " + qualification);

            //Employer Contact Number
            Console.Write("Enter Employer Contact Number");
            String employerContactNumber = Console.ReadLine();
            streamWriter.WriteLine("employerContactNumber: " + employerContactNumber);

            //Occupation
            Console.Write("Enter Occupation/Profession");
            String occupation = Console.ReadLine();
            streamWriter.WriteLine("occupation: " + occupation);

            //staffNumber
            Console.Write("Enter Employment/Staff Number");
            String staffNumber = Console.ReadLine();
            streamWriter.WriteLine("staffNumber: " + staffNumber);

            //Contact Nature
            Console.Write("Enter Contact Nature");
            String contractNature = Console.ReadLine();
            streamWriter.WriteLine("contractNature: " + contractNature);

            //Contact Expiry
            Console.Write("Enter Contact Expiry");
            String contractExpiry = Console.ReadLine();
            streamWriter.WriteLine("contractExpiry: " + contractExpiry);

            //duration
            Console.Write("Enter Duration at current organisation");
            String durationAtOrganisation = Console.ReadLine();
            streamWriter.WriteLine("durationAtOrganisation: " + durationAtOrganisation);

            streamWriter.Close();
        }
    }
}
