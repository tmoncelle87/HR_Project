﻿using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Channels; // Added for state abbreviation check
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

namespace Program
{
    public class Employer
    {

        // Properties to store employer information
        public string EmployerName { get; set; } = null;
        public string EmployerTaxId { get; set; } = null;
        public string EmployerStreetAddress { get; set; } = null;
        public string EmployerCity { get; set; } = null;
        public string EmployerState { get; set; } = null;
        public string EmployerZipCode { get; set; } = null;
        public int EmployerNumberOfEmployees { get; set; } = 0;
        public string EmployerEmailAddress { get; set; } = null;
        public string EmployerPhoneNumber { get; set; } = null;
        public string EmployerLegalEntityType { get; set; } = null;
        public string HREmailAddress { get; set; } = null;
        public string HRPhoneNumber { get; set; } = null;
        public string EmployerBenefits { get; set; } = null;
        public string EmployerIndustry { get; set; } = null;

        // Override ToString() to provide a formatted string representation of the Employer object
        public override string ToString()
        {
            return $"EMPLOYER INFORMATION\n" +
                   $"Employer ID: {EmployerTaxId}\n" +
                   $"Employer Full Name: {EmployerName}\n" +
                   $"Employer Legal Entity Type: {EmployerLegalEntityType}\n" +
                   $"Industry: {EmployerIndustry}\n" +
                   $"Employer Address\n" +
                   $"{EmployerStreetAddress}\n" +
                   $"{EmployerCity.ToUpper()} {EmployerState.ToUpper()} {EmployerZipCode}\n" +
                   $"Number Of Employees: {EmployerNumberOfEmployees}\n" +
                   "Employer Contact Information\n" +
                   $"Employer Telephone Number: {EmployerPhoneNumber}\n" +
                   $"Employer Email Address: {EmployerEmailAddress}\n" +
                   "Employer Benefits\n" +
                   $"{EmployerBenefits}\n" +
                   "HR Contact Information\n" +
                   $"HR Email Address: {HREmailAddress}\n" +
                   $"HR Telephone Number: {HRPhoneNumber}\n";


        }
    }
    public class Methods
    {

        static HashSet<string> stateAbbreviations = new HashSet<string>
        {
            "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA",
            "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD",
            "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ",
            "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC",
            "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY",
            "AS", "DC", "FM", "GU", "MH", "MP", "PR", "PW", "VI"
        };

        static string PromptForValidState(string prompt)
        {
            string state;

            // Keep prompting until a valid state abbreviation is entered
            while (true)
            {
                state = PromptUser(prompt).ToUpper();
                if (stateAbbreviations.Contains(state))
                    return state;

                Console.WriteLine("Invalid input, make sure to enter a valid state in abbreviation form");
            }
        }




        static string PromptUser(string prompt)
        {
            string input;
            do
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input cannot be empty. Please try again.");
                }
            } while (string.IsNullOrEmpty(input));
            return input;
        }




        public static string PromptFormatEmailAddress(string prompt)
        {
            while (true)
            {
                string userInputEmail = PromptUser(prompt);
                try
                {
                    MailAddress mailAddress = new MailAddress(userInputEmail);
                    return mailAddress.Address; // Return the normalized email address
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid email address format. Please try again.");
                }
            }
        }


        public static bool VerifyDecimal(string input, out decimal verifiedDecimal)
        {
            if (decimal.TryParse(input, out verifiedDecimal))
            {
                if (verifiedDecimal >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }




        public static bool VerifyInt(string input, out int verifiedInt)
        {
            if (int.TryParse(input, out verifiedInt))
            {
                if (verifiedInt >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            
        }

        public static string PromptFormatTelephoneNumber(string prompt)
        {
            while (true)
            {
                PromptUser(prompt + " telephone format: +11234567891 or 1234567891");
                string telephoneNumber = Console.ReadLine();

                // Using VerifyInt method to check if the input is numeric
                int verifiedInt;
                if (VerifyInt(telephoneNumber, out verifiedInt))
                {
                    // Check length and format based on length
                    if (telephoneNumber.Length == 10)
                    {
                        // Format 10-digit number as xxx-xxx-xxxx
                        return telephoneNumber.Substring(0, 3) + "-" + telephoneNumber.Substring(3, 3) + "-" + telephoneNumber.Substring(6, 4);
                    }
                    else if (telephoneNumber.Length == 12)
                    {
                        // Format 12-digit number with the leading + as +x-xxx-xxx-xxxx
                        return telephoneNumber.Substring(0, 1) + "-" + telephoneNumber.Substring(2, 3) + "-" + telephoneNumber.Substring(5, 3) + "-" + telephoneNumber.Substring(8, 4);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please ensure the number has 10 or 12 digits.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter numeric characters only.");
                }
            }
        }



        public static string PromptFormatZipCode(string prompt)
        {
            while (true)
            {   
                PromptUser(prompt + " Zip Code format(12345 or 123456789)");
                int verifiedInt;
                string  zipCode = Console.ReadLine();
                if (VerifyInt(zipCode, out verifiedInt))
                    {
                        if (zipCode.Length == 5)
                        {
                            return zipCode;
                        }
                        else if (zipCode.Length == 9)
                         {
                            return zipCode.Substring(0, 5) + "-" + zipCode.Substring(5, 4);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Zipcode input, Please make sure to enter a zipcode according to the format(12345 or 123456789):");
                         }

                    }
                else
                {
                    Console.WriteLine("Invalid input, Please make sure to only input numbers for zipcode:");
                }
               

            }
            static bool promptForYesNo(string prompt)
            {
                Console.WriteLine(prompt);
                string input;
                while (true)
                {
                    input = PromptUser("Please type and enter 'y' for yes or 'n' for no:").ToLower();
                    if (input == "y")
                    {
                        return true;
                    }
                    else if (input == "n")
                    {
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please enter 'y' for yes or 'n' for no.");
                    }
                }
            }
            bool isSalary = false;
            bool isInvalidInput = true;
            string userInput = null;

            bool isEmployed = false;
            string employedInput;
            
            // Prompt user for employment status
            while (true)
            {
                employedInput = PromptUser("Are you currently employed? (Enter y for yes or n for no): ").ToLower();

                // Declare a boolean to store employment status


                // Determine employment status based on user input
                if (employedInput == "y")
                {
                    isEmployed = true;
                    break;
                }
                else if (employedInput == "n")
                {

                    isEmployed = false;
                    break;

                }
                Console.WriteLine("Invalid input, make sure to enter a proper response either a 'y' for yes or 'n' for no");

            }

        }

        public class EmployeeData
        {
            // Properties to store employee information

            /*
    public enum EmployeePosition
    {
        CEO,
        CFO,
        COO,
        MarketingManager,
        SalesManager,
        ProjectManager,
        HRManager,
        FinancialAnalyst,
        Accountant,
        CustomerServiceRepresentative,
        AdministrativeAssistant,
        SoftwareEngineer,
        ITSpecialist,
        ProductManager,
        GraphicDesigner,
        ContentWriter,
        OperationsManager,
        BusinessAnalyst,
        SupplyChainManager,
        LegalCounsel
    }
            public EmployeePosition Position { get; set; }
      */

            public string EmployeeFirstName { get; set; } = null;
            public string EmployeeLastName { get; set; } = null;
            public string EmployeeStreetAddress { get; set; } = null;
            public string EmployeeCity { get; set; } = null;
            public string EmployeeState { get; set; } = null;
            public string EmployeeZipCode { get; set; } = null;

            public string EmployeePosition { get; set; } = null;
            public string EmployeeId { get; set; } = null;
            public decimal EmployeeSalary { get; set; } = 0;
            public decimal EmployeeAverageHoursWorkedWeekly { get; set; } = 0;
            public decimal EmployeeHourlyPay { get; set; } = 0;
            public decimal EmployeeHourlyYearlyPay { get; set; } = 0;
            public string EmployeePhoneNumber { get; set; } = null;
            public string EmployeeEmailAddress { get; set; } = null;
            public string EmployeeDepartment { get; set; } = null;
            public string EmployeeEmploymentSatus { get; set; } = null;
            public string EmployeePerformanceRating { get; set; } = null;
            public string EmployeeOfficeStreetAddress { get; set; } = null;
            public string EmployeeOfficeCity { get; set; } = null;
            public string EmployeeOfficeState { get; set; } = null;
            public string EmployeeOfficeZipCode { get; set; } = null;

            // Property to hold an Employer object, representing the employee's employer
            public Employer Employer { get; set; }

            public bool isSalary { get; set; } // Added isSalary property
            public bool hasOfficeLocation { get; set; }

            // Override ToString() to provide a formatted string representation of the EmployeeData object
            public override string ToString()
            {
                string salaryInfo;
                string officeLocation = null; // Initialize to null

                // If the employee is salaried, show their yearly salary
                if (isSalary == true)
                {
                    salaryInfo = $"Employee Yearly Salary: {EmployeeSalary}\n";
                }
                else
                {
                    salaryInfo = $"Employee Yearly Pay: {EmployeeHourlyYearlyPay}\n";
                }

                // If the employee has an office location, display the office address
                if (hasOfficeLocation == true)
                {
                    officeLocation = $"Office Address: {EmployeeOfficeStreetAddress}\n" +
                                     $"{EmployeeOfficeCity.ToUpper()} {EmployeeOfficeState.ToUpper()} {EmployeeOfficeZipCode}\n";
                }

                return $"{Employer}\n" +
                       "EMPLOYEE INFORMATION\n" +
                       $"Employee Current Department: {EmployeeDepartment}\n" +
                       $"Employment Status: {EmployeeEmploymentSatus}\n" +
                       $"Employee Position: {EmployeePosition}\n" +
                       $"Employee ID: {EmployeeId}\n" +
                       $"Employee First and Last Name: {EmployeeFirstName} {EmployeeLastName}\n" +
                       $"Employee Current Address\n" +
                       $"{EmployeeStreetAddress}\n" +
                       $"{EmployeeCity.ToUpper()} {EmployeeState.ToUpper()} {EmployeeZipCode}\n\n" +
                       officeLocation + // Display office location once
                       $"Performance: {EmployeePerformanceRating}\n" +
                       salaryInfo +
                       $"Employee Contact Information\n" +
                       $"Phone Number: {EmployeePhoneNumber}\n" +
                       $"Email Address: {EmployeeEmailAddress}\n";
            }
        }

        class Program
        {
            // Array to store valid state abbreviations
           
            static void Main(string[] args)
            {
                // Declare a string to store user input
                // Helper method for prompting the user and reading input



                
                bool isSalary = false;
                bool isInvalidInput = true;
                string userInput = null;

                bool isEmployed = false;
                string employedInput;
                // Prompt user for employment status
                while (true)
                {
                    employedInput = PromptUser("Are you currently employed? (Enter y for yes or n for no): ").ToLower();

                    // Declare a boolean to store employment status


                    // Determine employment status based on user input
                    if (employedInput == "y")
                    {
                        isEmployed = true;
                        break;
                    }
                    else if (employedInput == "n")
                    {

                        isEmployed = false;
                        break;

                    }
                    Console.WriteLine("Invalid input, make sure to enter a proper response either a 'y' for yes or 'n' for no");

                }




                // Create an EmployeeData object to store employee information
                EmployeeData firstEmployee = new EmployeeData();

                // If employed, collect employer information
                if (isEmployed == true) 
                {
                    Console.WriteLine("Please answer the following questions about your employer:");
                    Employer firstEmployerObj = new Employer(); // Create Employer object
                    firstEmployee.Employer = firstEmployerObj; // Assign Employer to EmployeeData

                    // Prompt and collect employer information
                    firstEmployerObj.EmployerName = PromptUser("Please enter your employer's name:");
                    firstEmployerObj.EmployerTaxId = PromptUser("Please type and enter your employer's Tax Id:");
                    firstEmployerObj.HRPhoneNumber = PromptFormatTelephoneNumber("Please type and enter HR's telephone number:");
                    firstEmployerObj.HREmailAddress = PromptFormatEmailAddress("Please type and enter HR's Email address:");
                    firstEmployerObj.EmployerStreetAddress = PromptUser("Please type and enter your employer's street address:");
                    firstEmployerObj.EmployerCity = PromptUser("Please type and enter your employer's City:");
                    firstEmployerObj.EmployerState = PromptForValidState("Please type and enter your employer's state abbreviation:");
                    firstEmployerObj.EmployerZipCode = PromptFormatZipCode("Please type and enter your employer's zip code:");

                    string employeeNumberOFEmployees = null;
                    isInvalidInput = true;
                    int verifiedInt;

                    // Validate employee number input using a while loop
                    while (isInvalidInput)
                    {
                        userInput = PromptUser("Please type and enter your employer's number of employees:");

                        if (VerifyInt(userInput, out verifiedInt))
                        {
                            firstEmployerObj.EmployerNumberOfEmployees = verifiedInt;
                            isInvalidInput = false;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input, make sure to only type number.");
                        }
                    }

                    firstEmployerObj.EmployerEmailAddress =  PromptFormatEmailAddress("Please type and enter your employer's email address:");
                    firstEmployerObj.EmployerPhoneNumber = PromptUser("Please type and enter your employer's phone number (e.g., +1 123 456 7890):");
                    firstEmployerObj.EmployerLegalEntityType = PromptUser("Please type and enter your employer's legal entity type for the business:");
                    firstEmployee.EmployeePosition = PromptUser("Please type and enter your position:");


                    firstEmployee.EmployeeId = PromptUser("Please enter your employee Id:");


                    string UserSalaryInput = PromptUser("Are you a salary employee? Enter y for yes or n for no").ToUpper();

                    while (UserSalaryInput != "Y" && UserSalaryInput != "N")
                    {
                        // Inform the user that their input is invalid
                        Console.WriteLine("Invalid input, make sure to enter 'y' for yes or 'n' for no.");
                        UserSalaryInput = PromptUser("Are you a salary employee? Enter y for yes or n for no").ToUpper();
                    }
                    // Set isSalary based on valid input
                    if (UserSalaryInput == "Y")
                    {
                        isSalary = true;
                    }
                    else
                    {
                        isSalary = false;
                    }

                    firstEmployee.isSalary = isSalary; // Set isSalary property

                    // Collect salary information if salaried
                    if (isSalary == true)
                    {
                        decimal EmployeeYearlySalary = 0;
                        isInvalidInput = true;

                        // Validate salary input using a while loop
                        while (isInvalidInput)
                        {
                            userInput = PromptUser("Please type and enter your yearly salary:");

                            if (decimal.TryParse(userInput, out EmployeeYearlySalary))
                            {
                                firstEmployee.EmployeeSalary = EmployeeYearlySalary;
                                isInvalidInput = false;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input, make sure to enter numbers for your salary");
                            }
                        }
                    }

                    // Collect hourly information if not salaried
                    if (isSalary == false)
                    {
                        decimal EmployeeAverageHoursWorked;
                        isInvalidInput = true;

                        // Validate hours worked input using a while loop
                        while (isInvalidInput)
                        {
                            userInput = PromptUser("Please enter your average amount of hours you work weekly:");

                            if (decimal.TryParse(userInput, out EmployeeAverageHoursWorked))
                            {
                                firstEmployee.EmployeeAverageHoursWorkedWeekly = EmployeeAverageHoursWorked;
                                isInvalidInput = false;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input, make sure to enter numbers for your average amount of hours worked.");
                            }
                        }

                        // Prompt and collect hourly pay information
                        decimal EmployeeHourlyPay = 0;
                        isInvalidInput = true;

                        // Validate hours worked input using a while loop
                        while (isInvalidInput)
                        {
                            userInput = PromptUser("Please enter your hourly pay:");

                            if (decimal.TryParse(userInput, out EmployeeHourlyPay))
                            {
                                firstEmployee.EmployeeHourlyPay = EmployeeHourlyPay;
                                isInvalidInput = false;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input, make sure to only enter numbers for your hourly pay");
                            }
                        }

                        firstEmployee.EmployeeHourlyYearlyPay = (firstEmployee.EmployeeHourlyPay * firstEmployee.EmployeeAverageHoursWorkedWeekly * 52);
                    }






                }

                // Collect employee personal information
                firstEmployee.EmployeeFirstName = PromptUser("Please enter your first name:");
                firstEmployee.EmployeeLastName = PromptUser("Please enter your last name:");
                firstEmployee.EmployeeStreetAddress = PromptUser("Please enter your street address:");
                firstEmployee.EmployeeCity = PromptUser("Please enter your City of residence:");
                firstEmployee.EmployeeState = PromptForValidState("Please enter your state of residence abbreviated:").ToUpper();
                firstEmployee.EmployeeZipCode = PromptFormatZipCode("Please enter your zip code of residence:");


                // Prompt and collect remaining employee information
                firstEmployee.EmployeePhoneNumber = PromptUser("Please type and enter your preferred phone number:");
                firstEmployee.EmployeeEmailAddress = PromptFormatEmailAddress("Please type and enter your preferred email address:");
                firstEmployee.EmployeeDepartment = PromptUser("Please type and enter what department you work in:");
                firstEmployee.EmployeeEmploymentSatus = PromptUser("Please type and enter your employment status:");
                firstEmployee.EmployeePerformanceRating = PromptUser("Please type and enter your latest performance rating number:");

                Console.Clear(); // Clear the console

                // Prompt for office location information
                Console.WriteLine("Office Information");
                bool hasOfficeLocation = false;
                string userOfficeInput = PromptUser("Please type and enter y for yes or n for no if you have an office location:").ToUpper();

                // Determine if employee has an office location
                if (userOfficeInput == "Y")
                {
                    hasOfficeLocation = true;
                }
                else if (userOfficeInput == "N")
                {
                    hasOfficeLocation = false;
                }


                // Collect office location information if applicable
                if (hasOfficeLocation == true)
                {
                    firstEmployee.EmployeeOfficeStreetAddress = PromptUser("Please type and enter your office street address:");
                    firstEmployee.EmployeeOfficeCity = PromptUser("Please type and enter the city your office is located in:");
                    firstEmployee.EmployeeOfficeState = PromptForValidState("Please type and enter the state your office is located in").ToUpper();
                    // Prompt and collect office zip code
                    firstEmployee.EmployeeOfficeZipCode = PromptFormatZipCode("Please type and enter the zip code that your office is located in:");
                    Console.Clear();
                }

                // Clear the console and display employee information
                Console.Clear();
                Console.WriteLine(firstEmployee);
            }
        }
    }
}