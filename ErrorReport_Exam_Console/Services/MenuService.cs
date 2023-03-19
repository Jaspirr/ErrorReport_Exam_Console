using ErrorReport_Exam_Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Console.WriteLine("4. Update a Report");
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

            errorReport.ErrorReportStatus = "Not Started";
            

            await DataService.CreateAsync(errorReport);
            //Console.ReadKey();
        }

        private static async Task OptionTwoAsync()
        {
            var errorReports = await DataService.GetAllAsync();

            if (errorReports.Any())
            {
                Console.Clear();
                Console.WriteLine("Select an error report to view:");

                // Skriv ut en lista över alla ärenden med deras ID
                foreach (ErrorReport errorReport in errorReports)
                {
                    Console.WriteLine($"ID: {errorReport.ErrorReportId}, Report Title: {errorReport.Title}");
                }

                // Be användaren att ange ID för det ärende de vill titta närmare på
                Console.Write("Enter the ID of the error report you want to view: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    var errorReport = await DataService.GetOneAsync(id);
                    if (errorReport != null)
                    {
                        Console.Clear();
                        Console.WriteLine($"Id: {errorReport.ErrorReportId}");
                        Console.WriteLine($"Name: {errorReport.FirstName} {errorReport.LastName}");
                        Console.WriteLine($"Email: {errorReport.EmailAddress}");
                        Console.WriteLine($"Phonenumber: {errorReport.PhoneNumber}");
                        Console.WriteLine($"Report: {errorReport.Title}");
                        Console.WriteLine($"Description: {errorReport.Description}");
                        Console.WriteLine($"Report Status: {errorReport.ErrorReportStatus} \n");
                    }
                    else
                    {
                        Console.WriteLine("No error report found with that ID.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid ID.");
                }
            }
            else
            {
                Console.WriteLine("There are no error reports in the database.");
            }
        }
        private static async Task OptionThreeAsync()
        {

            Console.Write("Enter the ID on the Report: ");

            if (int.TryParse(Console.ReadLine(), out int value))
            {
                var errorReport = await DataService.GetOneAsync(value);

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
                    Console.WriteLine($"No Report with specified Id {value} was found.");
                    Console.ReadKey();


                }
            }
            else
            {
                Console.WriteLine("Not an ID number");
                Console.ReadKey();

            }

        }

        private static async Task OptionFourAsync()
        {
            Console.Write("Enter the ID of the Report you want to update: ");

            if (int.TryParse(Console.ReadLine(), out int value))
            {
                var errorReport = await DataService.GetOneAsync(value);
                if (errorReport != null)
                {
                    Console.Clear();
                    Console.WriteLine("Current Report Status: " + errorReport.ErrorReportStatus);

                    Console.WriteLine("Select new Status:");
                    Console.WriteLine("1. Not started");
                    Console.WriteLine("2. Open");
                    Console.WriteLine("3. Closed");

                    Console.Write("Enter choice: ");
                    if (int.TryParse(Console.ReadLine(), out int choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                errorReport.ErrorReportStatus = "Not started";
                                break;
                            case 2:
                                errorReport.ErrorReportStatus = "Open";
                                break;
                            case 3:
                                errorReport.ErrorReportStatus = "Closed";
                                break;
                            default:
                                Console.WriteLine("Invalid choice");
                                return;
                        }

                        await DataService.UpdateAsync(errorReport);
                        Console.WriteLine("Report updated successfully");
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice");
                    }
                }
                else
                {
                    Console.WriteLine("No Report found with this Id");
                }
            }
            else
            {
                Console.WriteLine("No ID specified");
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

                Console.Write("Enter the ID of the report you want to delete: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    var errorReportToDelete = await DataService.GetOneAsync(id);
                    if (errorReportToDelete != null)
                    {
                        await DataService.DeleteAsync(errorReportToDelete.ErrorReportId);
                        Console.WriteLine($"Report with ID {id} has been deleted");
                    }
                    else
                    {
                        Console.WriteLine("Report not found");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID");
                }
            }
            else
            {
                Console.WriteLine("No reports found");
            }
        }
    }
}
