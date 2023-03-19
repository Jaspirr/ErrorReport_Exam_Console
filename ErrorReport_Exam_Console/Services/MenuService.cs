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
                    Console.ReadKey();
                    break;

            }
        }

        private async Task OptionOneAsync()
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

        private async Task OptionTwoAsync()
        {

            var errorReports = await DataService.GetAllAsync();

            if (errorReports.Any())
            {
                foreach (ErrorReport errorReport in errorReports)
                {
                    Console.WriteLine($"Id: {errorReport.CustomerId}");
                    Console.WriteLine($"Name: {errorReport.FirstName} {errorReport.LastName}");
                    Console.WriteLine($"Email: {errorReport.EmailAddress}");
                    Console.WriteLine($"Phonenumber: {errorReport.PhoneNumber}");
                    Console.WriteLine($"Report: {errorReport.Title}");
                    Console.WriteLine($"Description: {errorReport.Description}");
                    Console.WriteLine($"Report Status: {errorReport.ErrorReportStatus} \n");
                }
            }



        }
        private async Task OptionThreeAsync()
        {

            Console.Write("Enter the ID on the Report: ");

            if (int.TryParse(Console.ReadLine(), out int value))
            {
                var errorReport = await DataService.GetOneAsync(value);

                if (errorReport != null)
                {
                    Console.WriteLine($"{errorReport.CustomerId}");
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

        private async Task OptionFourAsync()
        {
            Console.Write("Enter the ID of the Report you want to update: ");

            if (int.TryParse(Console.ReadLine(), out int value))
            {
                var errorReport = await DataService.GetOneAsync(value);
                if (errorReport != null)
                {
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

                    Console.Write("Change Report Status (Option : Not started > Open > Closed): ");
                    errorReport.ErrorReportStatus = Console.ReadLine() ?? "";

                    await DataService.UpdateAsync(errorReport);
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

        public async Task OptionFiveAsync()
        {
            Console.Write("Enter the ID of the Report you want to delete: ");

            if (int.TryParse(Console.ReadLine(), out int value))
            {

                var errorReport = await DataService.GetOneAsync(value);
                if (errorReport != null)
                {
                    await DataService.DeleteAsync(value);
                }
                else
                {
                    Console.WriteLine("Report with this Id was not found");
                }
            }
        }
    }
}
