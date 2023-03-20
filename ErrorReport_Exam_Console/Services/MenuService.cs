using ErrorReport_Exam_Console.Models;
using ErrorReport_Exam_Console.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ErrorReport_Exam_Console.Services
{
    internal class MenuService
    {
        public async void MainMenu()
        {


            Console.Clear();
            Console.WriteLine("1. Create a Report");
            Console.WriteLine("2. Show all Reports");
            Console.WriteLine("3. Show a specific Report");
            Console.WriteLine("4. Update a Report Status");
            Console.WriteLine("5. Delete a Report");
            var option = Console.ReadLine();


            switch (option)
            {
                case "1":
                    Console.Clear();
                    await OptionOneAsync(); ;
                    Console.ReadKey();
                    break;
                case "2":
                    Console.Clear();
                    await OptionTwoAsync();
                    Console.ReadKey();
                    break;
                case "3":
                    Console.Clear();
                    await OptionThreeAsync();
                    Console.ReadKey();
                    break;
                case "4":
                    Console.Clear();
                    await OptionFourAsync();
                    Console.ReadKey();
                    break;
                case "5":
                    Console.Clear();
                    await OptionFiveAsync();
                    Console.ReadKey();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Not an option");
                    Console.WriteLine("Press any key to continue...");
                    break;

            }
        }

        private static async Task OptionOneAsync()
        {
            var errorReport = new ErrorReport();

            Console.WriteLine("Add a Report");

            Console.Write("First Name: ");
            errorReport.FirstName = Console.ReadLine() ?? "";

            Console.Write("Last Name: ");
            errorReport.LastName = Console.ReadLine() ?? "";

            Console.Write("Email Address: ");
            errorReport.EmailAddress = Console.ReadLine() ?? "";

            Console.Write("Phonenumber: ");
            errorReport.PhoneNumber = Console.ReadLine() ?? "";

            Console.Write("Report Title: ");
            errorReport.Title = Console.ReadLine() ?? "";

            Console.Write("Description: ");
            errorReport.Description = Console.ReadLine() ?? "";


            await DataService.CreateAsync(errorReport);
            //Console.ReadKey();
        }

        private static async Task OptionTwoAsync()
        {
            var errorReports = await DataService.GetAllAsync();

            if (errorReports.Any())
            {

                foreach (ErrorReport errorReport in errorReports)
                {
                    Console.WriteLine($"ID: {errorReport.ErrorReportId}");
                    Console.WriteLine($"Name: {errorReport.FirstName} {errorReport.LastName}");
                    Console.WriteLine($"Email Address: {errorReport.EmailAddress}");
                    Console.WriteLine($"Phonenumber: {errorReport.PhoneNumber}");
                    Console.WriteLine($"Report Title: {errorReport.Title}");
                    Console.WriteLine($"Description: {errorReport.Description}");
                    Console.WriteLine($"Created: {errorReport.Time}");
                    Console.WriteLine($"Status: {errorReport.ErrorReportStatus}");
                    Console.WriteLine("");
                }
            }
        
            else
            {
                Console.WriteLine("There are no error reports in the database.");
                Console.WriteLine("");
            }
        }
        private static async Task OptionThreeAsync()
        {

            Console.Write("Enter the Email Address on the Report: ");

            var emailAddress = Console.ReadLine();

            if (!string.IsNullOrEmpty(emailAddress))
            {
                var errorReport = await DataService.GetOneAsync(emailAddress);
                if (errorReport != null)
                {
                    Console.WriteLine($"{errorReport.ErrorReportId}");
                    Console.WriteLine($"Name: {errorReport.FirstName} {errorReport.LastName}");
                    Console.WriteLine($"Email: {errorReport.EmailAddress}");
                    Console.WriteLine($"Phonenumber: {errorReport.PhoneNumber}");
                    Console.WriteLine($"Report: {errorReport.Title}");
                    Console.WriteLine($"Description: {errorReport.Description}");
                    Console.WriteLine($"Report Status: {errorReport.ErrorReportStatus} \n");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine($"No Report with specified Email Address {emailAddress} was found.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Not an Email Address number");
                Console.ReadKey();

            }

        }

        private static async Task OptionFourAsync()
        {
            Console.Write("Enter the Email Address of the Report you want to update: ");
            var emailAddress = Console.ReadLine();

            if (!string.IsNullOrEmpty(emailAddress))
            {
                var errorReport = await DataService.GetOneAsync(emailAddress);
                if (errorReport != null)
                {
                    Console.Clear();
                    Console.WriteLine("Current Report Status: " + errorReport.ErrorReportStatus);

                    Console.WriteLine("Select new Status with the starting number:");
                    Console.WriteLine("0. Not started");
                    Console.WriteLine("1. Open");
                    Console.WriteLine("2. Closed");

                    Console.Write("Enter choice: ");
                    var test = Console.ReadLine() ?? null!;
                    if (!string.IsNullOrEmpty(test))
                    {
                        errorReport.ErrorReportStatus = Enum.Parse<ErrorReportStatus>(test);
                        await DataService.UpdateStatusAsync(errorReport);
                    }
                    else
                    {
                        Console.WriteLine($"Status är oförändrad.");
                        Console.WriteLine("");
                    }
                }
                else
                {
                    Console.WriteLine("No Report found with this Email Address");
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine("No Email Address specified");
            }
        }

        private static async Task OptionFiveAsync()
        {
            var errorReports = await DataService.GetAllAsync();

            if (errorReports.Any())
            {
                Console.Clear();
                Console.WriteLine("All reports:");
                foreach (var errorReport in errorReports)
                {
                    Console.WriteLine($"Id: {errorReport.ErrorReportId}");
                    Console.WriteLine($"Name: {errorReport.FirstName} {errorReport.LastName}");
                    Console.WriteLine($"Email: {errorReport.EmailAddress}");
                    Console.WriteLine($"Phonenumber: {errorReport.PhoneNumber}");
                    Console.WriteLine($"Report: {errorReport.Title}");
                    Console.WriteLine($"Description: {errorReport.Description}");
                    Console.WriteLine($"Report Status: {errorReport.ErrorReportStatus} \n");
                }

                Console.Write("Enter the Email Address of the report you want to delete: ");
                var emailAddress = Console.ReadLine();

                if (!string.IsNullOrEmpty(emailAddress))
                {
                    var errorReport = await DataService.GetOneAsync(emailAddress);
                    if (!string.IsNullOrEmpty(emailAddress))
                    {
                        await DataService.DeleteAsync(emailAddress);
                        Console.WriteLine($"Report with Email Address {emailAddress} has been deleted");
                    }
                    else
                    {
                        Console.WriteLine("Report not found");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Email Address");
                }
            }
            else
            {
                Console.WriteLine("No reports found");
            }
        }
    }
}
