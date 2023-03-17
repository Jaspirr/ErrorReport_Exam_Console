using ErrorReport_Exam_Console.Services;
using ErrorReport_Exam_Console.Contexts;


var menu = new MainService();


while (true)
{

    Console.Clear();
    Console.WriteLine("1. Create a new ticket");
    Console.WriteLine("2. Show all tickets");
    Console.WriteLine("3. Show a specific ticket");
    Console.WriteLine("4. Update a specific ticket");
    Console.WriteLine("5. Delete a specific ticket");
    Console.Write("Choose one of the following options(1-5): ");


    switch (Console.ReadLine())
    {
        case "1":
            Console.Clear();
            await menu.CreateErrorReportAsync();
            break;

        case "2":
            Console.Clear();
            await menu.ListAllErrorReportsAsync();
            break;

        case "3":
            Console.Clear();
            await menu.ShowSpecificErrorReportAsync();
            break;

        case "4":
            Console.Clear();
            await menu.UpdateSpecificErrorReportAsync();
            break;

        case "5":
            Console.Clear();
            await menu.DeleteSpecificErrorReportAsync();
            break;
    }

    Console.WriteLine("\nPush any key to continue...");
    Console.ReadKey();

}
