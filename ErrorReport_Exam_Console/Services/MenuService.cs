using ErrorReport_Exam_Console.Models;
using ErrorReport_Exam_Console.Models.Entities;
using Microsoft.IdentityModel.Tokens;
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
        public static async void MainMenu()
        {


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
                case "6":
                    Console.Clear();
                    await OptionSixAsync();
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

            Console.Write("Enter the Id on the Report: ");


            if (int.TryParse(Console.ReadLine(), out int value))
            {
                var errorReport = await DataService.GetOneAsync(value);
                if (errorReport != null)
                {
                    Console.WriteLine($"{errorReport.ErrorReportId}");
                    Console.WriteLine($"Name: {errorReport.Customer.FirstName} {errorReport.Customer.LastName}");
                    Console.WriteLine($"Email: {errorReport.Customer.EmailAddress}");
                    Console.WriteLine($"Phonenumber: {errorReport.Customer.PhoneNumber}");
                    Console.WriteLine($"Report: {errorReport.Title}");
                    Console.WriteLine($"Description: {errorReport.Description}");
                    Console.WriteLine($"Report Status: {errorReport.ErrorReportStatus} \n");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine($"No Report with specified Id {value} was found.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("That Id was not found ");
                Console.ReadKey();

            }

        }

        private static async Task OptionFourAsync()
        {
            Console.Write("Enter the Id of the Report you want to update: ");

            if (int.TryParse(Console.ReadLine(), out int value))
            {
                var errorReport = await DataService.GetOneAsync(value);
                if (errorReport != null)
                {
                    Console.WriteLine("Add a Report");

                    Console.Write("First Name: ");
                    errorReport.Customer.FirstName = Console.ReadLine() ?? "";

                    Console.Write("Last Name: ");
                    errorReport.Customer.LastName = Console.ReadLine() ?? "";

                    Console.Write("Email Address: ");
                    errorReport.Customer.EmailAddress = Console.ReadLine() ?? "";

                    Console.Write("Phonenumber: ");
                    errorReport.Customer.PhoneNumber = Console.ReadLine() ?? "";

                    Console.Write("Title: ");
                    errorReport.Title = Console.ReadLine() ?? "";

                    Console.Write("Description: ");
                    errorReport.Description = Console.ReadLine() ?? "";

                    Console.Clear();
                    Console.WriteLine("Current Report Status: " + errorReport.ErrorReportStatus);

                    Console.WriteLine("Select new Status with the starting number:");
                    Console.WriteLine("0. Not started");
                    Console.WriteLine("1. Open");
                    Console.WriteLine("2. Closed");

                    string status = Console.ReadLine() ?? "";

                    do
                    {
                        switch (status)
                        {
                            case "1":
                                errorReport.ErrorReportStatus = "Ej Påbörjad";
                                break;
                            case "2":
                                errorReport.ErrorReportStatus = "Pågående";
                                break;
                            case "3":
                                errorReport.ErrorReportStatus = "Avslutad";
                                break;
                            default:
                                Console.WriteLine("Inte ett alternativ");
                                break;
                        }
                    }
                    while (status != "1" && status != "2" && status != "3");


                    await DataService.UpdateAsync(errorReport);
                }
                else
                {
                    Console.WriteLine("Invalid Id");
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine("You must write an Id of a report");
                Console.WriteLine("");
            }
        }

        private static async Task OptionFiveAsync()
        {
            var errorReports = await DataService.GetAllAsync();

            if (errorReports.Any())
            {
                foreach (ErrorReport errorReport in errorReports)
                {
                    Console.WriteLine($"Id: {errorReport.ErrorReportId}");
                    Console.WriteLine($"Name: {errorReport.FirstName} {errorReport.LastName}");
                    Console.WriteLine($"Email: {errorReport.EmailAddress}");
                    Console.WriteLine($"Phonenumber: {errorReport.PhoneNumber}");
                    Console.WriteLine($"Report: {errorReport.Title}");
                    Console.WriteLine($"Description: {errorReport.Description}");
                    Console.WriteLine($"Report Status: {errorReport.ErrorReportStatus} \n");
                }
            }
            else
            {
                Console.WriteLine("No Reports found");
                Console.WriteLine("");
            }




        }
        private static async Task OptionSixAsync()
        {
            var comment = new CommentModel();

            Console.WriteLine("Enter the Id of the report you want to comment:");
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                var errorReport = await DataService.GetOneAsync(value);
                if (errorReport != null)
                {
                    Console.WriteLine("Comment: ");
                    comment.Comment = Console.ReadLine() ?? "";
                    comment.CommentId = value;
                    await DataService.AddCommentAsync(comment);
                }
                else
                    Console.WriteLine("No valid Id was found");

            }
            else
                Console.WriteLine("You must write an Id of a report");

        }
    }
}
