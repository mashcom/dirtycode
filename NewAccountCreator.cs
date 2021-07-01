using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StandardChartered {
    class NewAccountCreator {

        public Dictionary<string, string> accountDetails = new Dictionary<string, string>();
        static string activeAccount;
        static string accountDirectory = Directory.GetCurrentDirectory();

        static void Main(string[] args) {

        h:
            
            Console.WriteLine(Validator.dateValidation(Console.ReadLine()));
            goto h;
            Console.WriteLine("************************************************************************************************");
            Console.WriteLine("WELCOME TO STANDARD CHARTERED BANK, SIMULATION");
            Console.WriteLine("Account Directory: " + accountDirectory);
            Console.WriteLine("************************************************************************************************");

        Start:
            Console.WriteLine("PLEASE SELECT ONE ITEM ON THE MENU BELOW:");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Balance Enquiry");
            Console.WriteLine("3. Deposit Funds");
            Console.WriteLine("4. Withdraw Funds");
            Console.WriteLine("5. Get Account Record");

            switch (Console.ReadLine()) {
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
                case "5":
                    getAcountRecordFromMenu();
                    break;
                default:
                    Console.WriteLine("Invalid Option");
                    goto Start;
            }
            goto Start;

        }

        static string generateAccountNumber() {
            Random accountNumberGenerator = new Random();
            int accountNumberStart = accountNumberGenerator.Next(111111, 999999);
            int accountNumberEnd = accountNumberGenerator.Next(11111, 99999);
            String uniqueAccountNumber = accountNumberStart.ToString() + accountNumberEnd.ToString();

            return uniqueAccountNumber;
        }

        static void getAcountRecordFromMenu() {
        StartRecord:
            Console.WriteLine("Please enter the 11 digit account number:");
            String accountNumber = Console.ReadLine();
            activeAccount = accountNumber;
            float balance;
            try {
                balance = getStoredBalance(activeAccount);
                getAccountRecord(activeAccount);
            } catch (FileNotFoundException ex) {
                Console.WriteLine("Account not found!");
                goto StartRecord;
            }
        }
        static void getAccountRecord(String accountNumber) {
            StreamReader reader = new StreamReader(accountDirectory + accountNumber + ".txt");

            Console.WriteLine("******************************************************************");
            Console.WriteLine("ACCOUNT RECORD FOR ACCOUNT NUMBER :" + accountNumber);
            Console.WriteLine("******************************************************************");
            while (reader.EndOfStream == false) {
                string line = reader.ReadLine();
                Console.WriteLine(line);
            }
            reader.Close();
            Console.WriteLine("*******************END OF ACCOUNT ***************************");
        }

        /**
         * Withdraw and Deposit funds into the account
         */
        static void changeBalance(String action) {

        StartDepositProcess:
            Console.WriteLine("Please enter the 11 digit account number:");
            String accountNumber = Console.ReadLine();
            activeAccount = accountNumber;
            float balance;
            try {
                balance = getStoredBalance(activeAccount);
            } catch (FileNotFoundException ex) {
                Console.WriteLine("Account not found!");
                goto StartDepositProcess;
            }

        EnterDepositAmount:
            Console.WriteLine("Please enter the amount you wish to " + action);
            String depositAmount = Console.ReadLine();
            float depositParsed;
            try {
                depositParsed = float.Parse(depositAmount);

                if (depositParsed <= 0) {
                    Console.WriteLine("Invalid input, amount should be greater than 0!");
                    goto EnterDepositAmount;
                }
                if (action == "withdraw") {
                    depositParsed = depositParsed * -1;
                }
                StreamWriter streamWriterForBalance = new StreamWriter(accountDirectory + activeAccount + "_balance.txt");
                float newBalance = balance + depositParsed;
                streamWriterForBalance.Write(newBalance);
                streamWriterForBalance.Close();

                Console.WriteLine("Your new balance:$" + newBalance);
                Console.WriteLine("Press any key to continue:");
                Console.ReadLine();

            } catch (Exception ex) {
                Console.WriteLine("Invalid amount format, please try again!");
                goto EnterDepositAmount;
            }
            Console.WriteLine(balance.ToString());


        }

        /**
         *Get balance from account file 
         */
        static float getStoredBalance(String accountNumber) {
            try {
                StreamReader reader = new StreamReader(accountDirectory + activeAccount + "_balance.txt");
                string balance = "";
                while (reader.EndOfStream == false) {
                    balance = reader.ReadLine();
                }
                reader.Close();
                return float.Parse(balance);
            } catch (Exception ex) {
                Console.WriteLine("Account not found!");
                return 0;
            }
        }

        static void balanceEnquiry() {
        StartEnquiryProcess:
            Console.WriteLine("Please enter the 11 digit account number:");
            String accountNumber = Console.ReadLine();
            activeAccount = accountNumber;
            float balance;
            try {
                balance = getStoredBalance(activeAccount);
                Console.WriteLine("Your current balance is $" + balance.ToString());
                Console.WriteLine("Press any key to continue:");
                Console.ReadLine();
            } catch (FileNotFoundException ex) {
                Console.WriteLine("Account not found!");
                goto StartEnquiryProcess;
            }
        }

        /**
         *Create new client account 
         */
        static void createAccount() {

            String accountNumber = generateAccountNumber();

            //create files for account details and account balance
            StreamWriter streamWriter = new StreamWriter(accountDirectory + accountNumber + ".txt");
            StreamWriter streamWriterForBalance = new StreamWriter(accountDirectory + accountNumber + "_balance.txt");

            streamWriterForBalance.WriteLine(0);//set the initial balance as 0
            streamWriterForBalance.Close();

            var newAccount = new NewAccountCreator();
            Console.WriteLine("1. PLEASE TELL US ABOUT YOURSELF?");

        //get title and validate the input
        EnterTitle:
            Console.Write("Enter Title:");
            String title = Console.ReadLine();
            String titleError = Validator.titleValidation(title);
            if (titleError != "") {
                Console.WriteLine(titleError);
                goto EnterTitle;
            }


            streamWriter.WriteLine("title: " + title);

        //get firstname and validate the input
        EnterFirstName:
            Console.Write("Enter First Name:");
            String firstName = Console.ReadLine();
            String firstNameError = Validator.NameValidation(firstName);
            if (firstNameError != "") {
                Console.WriteLine(firstNameError);
                goto EnterFirstName;
            }

            streamWriter.WriteLine("firstName: " + firstName);


        //get firstname and validate the input
        EnterMiddleName:
            Console.Write("Enter Middle Name (Optional):");
            String middleName = Console.ReadLine();
            String middleNameError = Validator.NameValidation(middleName, false);
            if (middleNameError != "") {
                Console.WriteLine(middleNameError);
                goto EnterMiddleName;
            }
            streamWriter.WriteLine("middleName: " + middleName);

        //get last and validate the input
        EnterLastName:
            Console.Write("Enter Last Name:");
            String lastName = Console.ReadLine();
            String lastNameError = Validator.NameValidation(lastName);
            if (lastNameError != "") {
                Console.WriteLine(lastNameError);
                goto EnterLastName;
            }
            streamWriter.WriteLine("lastName: " + lastName);


        //get residence status and validate
        EnterResidence:
            Console.Write("Are you a resident of Zimbabwe? ");
            String residence = Console.ReadLine().ToUpper();
            String residenceError = Validator.residenceValidation(residence);
            if (residenceError != "") {
                Console.WriteLine(residenceError);
                goto EnterResidence;
            }
            streamWriter.WriteLine("residence: " + residence);

            //request passport number if resident of Zimbabwe
            if (residence == "NO") {
            EnterPassportNumber:
                Console.Write("Enter passport number");
                String passportNumber = Console.ReadLine();
                if (passportNumber == "") {
                    Console.WriteLine("Please Enter Passport Number");
                    goto EnterPassportNumber;
                }
                streamWriter.WriteLine("passportNumber: " + passportNumber);
            }

        //get dob and validate the input
        EnterDateOfBirth:
            Console.Write("Enter Date of Birth Name:");
            String dateOfBirth = Console.ReadLine();
            String dateOfBirthError = Validator.dateValidation(dateOfBirth);
            if (dateOfBirthError != "") {
                Console.WriteLine(dateOfBirthError);
                goto EnterDateOfBirth;
            }
            streamWriter.WriteLine("dateOfBirth: " + dateOfBirth);

        EnterIdType:
            Console.Write("Enter ID Type:");
            Console.WriteLine("1. Passport  2. National ID  3. Driver's Licence");
            String idType = Console.ReadLine();
            String idTypeError = Validator.idTypeValidation(idType);
            if (idTypeError != "") {
                Console.WriteLine(idTypeError);
                goto EnterIdType;
            }
            streamWriter.WriteLine("idType: " + idType);


            //add  issue date date
            EnterIssueOrExpiryDate:
            Console.Write("Enter Issue/Expiry Date:");
            String issueOrExpiryDate = Console.ReadLine();
            String issueOrExpiryDateError = Validator.dateValidation(issueOrExpiryDate);
            if (issueOrExpiryDate != "") {
                Console.WriteLine(issueOrExpiryDateError);
                goto EnterIssueOrExpiryDate;
            }
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
            String gender = Console.ReadLine().ToUpper();
            if (gender != "MALE" && gender != "FEMALE") {
                Console.WriteLine("Gender should be Male or Female");
                goto EnterGender;
            }
            streamWriter.WriteLine("gender: " + gender);


        //add  marital status
        EnterMaritalStatus:
            Console.Write("Enter Enter Marital Status:");
            String maritalStatus = Console.ReadLine().ToUpper();
            if (maritalStatus == "") {
                Console.WriteLine("Marital Status is required");
                goto EnterMaritalStatus;
            }
            streamWriter.WriteLine("maritalStatus: " + maritalStatus);

        //spouse name
        EnterSpouseName:
            Console.Write("Enter Enter Spouse Name (Optional):");
            String spouseName = Console.ReadLine().ToUpper();
            String spouseNameError = Validator.NameValidation(spouseName, false);
            if (spouseNameError != "") {
                Console.WriteLine(spouseNameError);
                goto EnterSpouseName;
            }
            streamWriter.WriteLine("spouseName: " + spouseName);

        //number of children
        EnterNumberOfChildren:
            Console.Write("Enter Number of Children (Optional):");
            String numberOfChildren = Console.ReadLine();
            try {
                Validator.wholeNumberValidation(numberOfChildren);
                streamWriter.WriteLine("numberOfChildren: " + numberOfChildren);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                goto EnterNumberOfChildren;
            }

        //number of dependents
        EnterNumberOfDependents:
            Console.Write("Enter Number of Dependents (Optional): ");
            String numberOfDependents = Console.ReadLine();
            try {
                Validator.wholeNumberValidation(numberOfDependents, false);
                streamWriter.WriteLine("numberOfDependents: " + numberOfDependents);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                goto EnterNumberOfDependents;
            }


        //education
        EnterEducation:
            Console.Write("Enter Education: ");
            String education = Console.ReadLine();
            if (education.Length == 0) goto EnterEducation;
            streamWriter.WriteLine("education: " + education);

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("1A. CONTACT DETAILS?");
            Console.WriteLine("-------------------------------------------------");

        //country code
        EnterCountryCode:
            Console.Write("Enter Country Code:");
            String countryCode = Console.ReadLine();
            try {
                Validator.wholeNumberValidation(countryCode);
                if (countryCode.Length < 1 || countryCode.Length > 4) throw new FormatException("Country Code should have 2-4 digits");
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                goto EnterCountryCode;

            }
            streamWriter.WriteLine("countryCode: " + countryCode);

        //mobile
        EnterMobileNumber:
            Console.Write("Enter Mobile Number:");
            String mobileNumber = Console.ReadLine();
            try {
                Validator.phoneNumberValidation(mobileNumber);
                streamWriter.WriteLine("mobileNumber: " + mobileNumber);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                goto EnterMobileNumber;
            }


            //email
            Console.Write("Enter Email Address:");
            String emailAddress = Console.ReadLine();
            streamWriter.WriteLine("emailAddress: " + emailAddress);

        //mailing address
        EnterMailingAddress:
            Console.Write("Enter Mailing Address:");
            String mailingAddress = Console.ReadLine();
            if (mailingAddress.Length == 0) goto EnterMailingAddress;
            streamWriter.WriteLine("mailingAddress: " + mailingAddress);

        //ownership  residence
        EnterOnwnershipOfResidence:
            Console.Write("Enter Ownership of Residence:");
            String onwnershipOfResidence = Console.ReadLine();
            if (onwnershipOfResidence.Length == 0) goto EnterOnwnershipOfResidence;
            streamWriter.WriteLine("onwnershipOfResidence: " + onwnershipOfResidence);

        //rental amount
        EnterRentalAmount:
            Console.Write("If rented, Rental Amount:");
            String rentalAmount = Console.ReadLine();
            try {
                Validator.wholeNumberValidation(rentalAmount, false);
                streamWriter.WriteLine("rentalAmount: " + rentalAmount);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                goto EnterRentalAmount;
            }

        //Physical Residential Address
        EnterPhysicalAddress:
            Console.Write("Enter Physical Residential Address");
            String physicalAddress = Console.ReadLine();
            if (physicalAddress.Length == 0) goto EnterPhysicalAddress;
            streamWriter.WriteLine("physicalAddress: " + physicalAddress);

        //number of years
        EnterYearsAtResidence:
            Console.Write("Enter No. of Years Stayed :");
            float yearsAtResidence;
            try {
                yearsAtResidence = float.Parse(Console.ReadLine());
            } catch (Exception e) {
                Console.WriteLine("Invalid value for input Years At Residence");
                goto EnterYearsAtResidence;
            }

            streamWriter.WriteLine("yearsAtResidence: " + yearsAtResidence.ToString());

            if (yearsAtResidence < 3) {
            //Previous Residential Address
            EnterPreviousAddress:
                Console.WriteLine("Enter Previous Physical Residential Address: ");
                String previousAddress = Console.ReadLine();
                if (previousAddress.Length == 0) goto EnterPreviousAddress;
                streamWriter.WriteLine("previousAddress: " + previousAddress);
            }

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("1B. SOURCE OF FUNDS?");
            Console.WriteLine("-------------------------------------------------");

        //Nature Of Employment
        EnterNatureOfEmployment:
            Console.WriteLine("Enter Nature of Employment: e.g Salaried, Self-employed or any other");
            String natureOfEmployment = Console.ReadLine();
            if (natureOfEmployment.Length == 0) goto EnterNatureOfEmployment;
            streamWriter.WriteLine("natureOfEmployment: " + natureOfEmployment);

        //Nature Of Employment
        EnterEmploymentTerms:
            Console.WriteLine("Enter Employment Terms: e.g Permanent, Contract or any other:");
            String employmentTerms = Console.ReadLine();
            if (employmentTerms.Length == 0) goto EnterEmploymentTerms;
            streamWriter.WriteLine("employmentTerms: " + employmentTerms);

        //Name of Business
        EnterBusiness:
            Console.Write("Enter Name of Employer/Business");
            String business = Console.ReadLine();
            if (business.Length == 0) goto EnterBusiness;
            streamWriter.WriteLine("business: " + business);

        //Nature of Business
        EnterNatureOfBusiness:
            Console.Write("Enter Name of Employer/Business");
            String natureOfbusiness = Console.ReadLine();
            if (natureOfbusiness.Length == 0) goto EnterNatureOfBusiness;
            streamWriter.WriteLine("natureOfbusiness: " + natureOfbusiness);

        //Employers Address
        EnterEmployerAddress:
            Console.Write("Enter Employers Address");
            String employerAddress = Console.ReadLine();
            if (employerAddress.Length == 0) goto EnterEmployerAddress;
            streamWriter.WriteLine("employerAddress: " + employerAddress);

        //Qualification
        EnterQualification:
            Console.Write("Enter Qualification");
            String qualification = Console.ReadLine();
            if (qualification.Length == 0) goto EnterQualification;
            streamWriter.WriteLine("qualification: " + qualification);

        //Employer Contact Number
        EnterEmployerContactNumber:
            Console.Write("Enter Employer Contact Number");
            String employerContactNumber = Console.ReadLine();
            try {
                Validator.phoneNumberValidation(employerContactNumber);
                streamWriter.WriteLine("employerContactNumber: " + employerContactNumber);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                goto EnterEmployerContactNumber;
            }


        //Occupation
        EnterOccupation:
            Console.Write("Enter Occupation/Profession");
            String occupation = Console.ReadLine();
            if (occupation.Length == 0) goto EnterOccupation;
            streamWriter.WriteLine("occupation: " + occupation);

        //staffNumber
        EnterStaffNumber:
            Console.Write("Enter Employment/Staff Number");
            String staffNumber = Console.ReadLine();
            try {
                Validator.wholeNumberValidation(staffNumber, false);
                streamWriter.WriteLine("staffNumber: " + staffNumber);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                goto EnterStaffNumber;
            }

        //Contact Nature
        EnterContractNature:
            Console.Write("Enter Contract Nature");
            String contractNature = Console.ReadLine();
            if (contractNature.Length == 0) goto EnterContractNature;
            streamWriter.WriteLine("contractNature: " + contractNature);

            //Contact Expiry
            EnterContractExpiry:
            Console.Write("Enter Contract Expiry");
            String contractExpiry = Console.ReadLine();
            String contractExpiryError = Validator.dateValidation(contractExpiry);
            if (dateOfBirthError != "") {
                Console.WriteLine(contractExpiryError);
                goto EnterContractExpiry;
            }
            streamWriter.WriteLine("contractExpiry: " + contractExpiry);

        //duration
        EnterYearsAtOrganisation:
            Console.Write("Enter Years (You will be asked amount months on next input) at current organisation:");
            String durationAtOrganisation = Console.ReadLine();
            try {
                Validator.wholeNumberValidation(durationAtOrganisation);
                streamWriter.WriteLine("durationAtOrganisation: " + durationAtOrganisation);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                goto EnterYearsAtOrganisation;
            }

        //duration
        EnterMonthsAtOrganisation:
            Console.Write("Enter Months at current organisation:");
            String monthsAtOrganisation = Console.ReadLine();
            try {
                int month = Validator.wholeNumberValidation(monthsAtOrganisation);
                if (month < 1 || month > 12) throw new FormatException("Month input should be between 1 and 12");
                streamWriter.WriteLine("monthsAtOrganisation: " + monthsAtOrganisation);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                goto EnterMonthsAtOrganisation;
            }

            streamWriter.Close();

            Console.WriteLine("Your Account Created Successfully:");
            Console.WriteLine("ACCOUNT NUMBER: " + accountNumber);

            Console.WriteLine("Press any key to go to view full record?");
            getAccountRecord(accountNumber);
            Console.ReadLine();
            Console.WriteLine("Press any key to go to the main menu?");
            Console.ReadLine();
        }
    }
}
