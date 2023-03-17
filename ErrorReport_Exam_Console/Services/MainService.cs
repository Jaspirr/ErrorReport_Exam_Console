using ErrorReport_Exam_Console.Models.Entities;
using ErrorReport_Exam_Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ErrorReport_Exam_Console.Services
{
    public async Task CreateErrorReportAsync()
    {
        var errorReport = new ErrorReport();
        var customer = new Customer();

        Console.Write("Enter description of the ticket: ");
        errorReport.Description = Console.ReadLine() ?? "";

        Console.Write("Enter firstname of the customer: ");
        customer.FirstName = Console.ReadLine() ?? "";

        Console.Write("Enter lastname of the customer: ");
        customer.LastName = Console.ReadLine() ?? "";

        Console.Write("Enter email of the customer: ");
        customer.EmailAddress = Console.ReadLine() ?? "";

        Console.Write("Enter phonenumber of the customer: ");
        customer.PhoneNumber = Console.ReadLine() ?? "";


        errorReport.Status = ErrorReportStatus.NotStarted;

        errorReport.CreatedAt = DateTime.Now;

        // Saves ticket to the database
        await TicketService.SaveChangesAsync(errorReport, customer);

        Console.WriteLine("New ticket created successfully.");
    }


    public async Task ListAllErrorReportsAsync()
    {

        var errorReports = await ErrorReportService.GetAllAsync();

        if (errorReports.Any())
        {
            foreach (ErrorReport errorReport in errorReports)
            {
                Console.WriteLine($"Ticket ID: {errorReport.ErrorReportId}");
                Console.WriteLine($"Description: {errorReport.Description}");
                Console.WriteLine($"Status: {errorReport.ErrorReportStatus}");
                Console.WriteLine($"Customer ID: {errorReport.CustomerId}");
                Console.WriteLine($"Created At: {errorReport.CreatedAt}");

                Console.WriteLine("Comments:");

                var comments = await CommentService.GetAllCommentsAsync(errorReport.ErrorReportId);

                if (comments.Any())
                {
                    foreach (Comment comment in comments)
                    {
                        Console.WriteLine($"  Comment ID: {comment.CommentId}");
                        Console.WriteLine($"  Timestamp: {comment.Time}");
                        Console.WriteLine($"  Customer ID: {comment.CustomerId}");
                        Console.WriteLine($"  Ticket ID: {comment.ErrorReportId}");
                        Console.WriteLine($"  Text: {comment.Text}");
                    }
                }
                else
                {
                    Console.WriteLine("No comments for this ticket.");
                }

                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine("No tickets in the database.");
            Console.WriteLine("");
        }
    }


    public async Task ShowSpecificErrorReportAsync()
    {
        Console.Write("Please enter ticket ID: ");
        var errorReportId = int.Parse(Console.ReadLine() ?? "0");

        if (errorReportId != 0)
        {
            var errorReport = await ErrorReportService.GetAsync(errorReportId);

            if (errorReport != null)
            {
                Console.WriteLine($"Ticket ID: {errorReport.Id}");
                Console.WriteLine($"Description: {errorReport.Description}");
                Console.WriteLine($"Status: {errorReport.Status}");
                Console.WriteLine($"Customer ID: {errorReport.CustomerId}");
                Console.WriteLine($"Created At: {errorReport.CreatedAt}");

                Console.WriteLine("Comments:");

                Console.Write("Please enter comment ID: ");
                int commentId;
                if (!int.TryParse(Console.ReadLine(), out commentId))
                {
                    Console.WriteLine("Invalid comment ID. Please enter a valid integer.");
                    return;
                }

                var comments = await CommentService.GetCommentByIdAsync(errorReportId, commentId);

                if (comments != null)
                {
                    Console.WriteLine($"  Comment ID: {comments.Id}");
                    Console.WriteLine($"  Timestamp: {comments.Timestamp}");
                    Console.WriteLine($"  Customer ID: {comments.CustomerId}");
                    Console.WriteLine($"  Ticket ID: {comments.TicketId}");
                    Console.WriteLine($"  Text: {comments.Text}");
                }
                else
                {
                    Console.WriteLine($"No comment with ID {commentId} was found for ticket {errorReportId}.");
                }

                Console.WriteLine("");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"No ticket with ID {errorReportId} was found.");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine("No ticket ID was entered.");
            Console.WriteLine("");
        }
    }


    public async Task UpdateSpecificErrorReportAsync()
    {
        Console.Write("Enter ticket ID: ");
        var errorReportIdStr = Console.ReadLine();
        if (!int.TryParse(errorReportIdStr, out var errorReportId))
        {
            Console.WriteLine($"Invalid Error Report ID.");
            return;
        }

        var errorReport = await ErrorReportService.GetAsync(errorReportId);
        if (errorReport != null)
        {
            Console.WriteLine("Please enter the information in the fields you want to update. \n");

            Console.Write("Description of ticket: ");
            errorReport.Description = Console.ReadLine()!;

            Console.Write("Additional Comments for changes: ");
            var commentText = Console.ReadLine()!;

            var comment = new CommentEntity
            {
                Text = commentText,
                ErrorReportId = errorReportId
            };

            await CommentService.AddCommentAsync(comment);

            Console.Write("Status (Started, Closed): ");
            if (!Enum.TryParse(Console.ReadLine(), out ErrorReportStatus status))
            {
                Console.WriteLine($"Invalid status.");
                return;
            }
            errorReport.Status = status;

            await ErrorReportService.UpdateAsync(errorReport);
            Console.WriteLine($"Ticket {errorReport.Id} has been updated.");
        }
        else
        {
            Console.WriteLine($"Ticket with ID {errorReportId} not found.");
        }
    }



    public async Task DeleteSpecificErrorReportAsync()
    {
        Console.Write("Enter ticket ID: ");
        var ticketIdStr = Console.ReadLine();
        if (!int.TryParse(ticketIdStr, out var ticketId))
        {
            Console.WriteLine($"Invalid ticket ID.");
            return;
        }

        var ticket = await ErrorReportService.GetAsync(ticketId);
        if (ticket != null)
        {
            Console.WriteLine($"Are you sure you want to delete the following ticket? (Y/N) \n");
            Console.WriteLine($"Ticket ID: {ticket.Id}");
            Console.WriteLine($"Description: {ticket.Description}");
            Console.WriteLine($"Status: {ticket.Status}");
            Console.WriteLine($"Created: {ticket.CreatedAt}");
            Console.WriteLine($"Customer ID: {ticket.CustomerId}\n");

            var confirmation = Console.ReadLine()!.ToLower();
            if (confirmation == "y")
            {
                await ErrorReportService.DeleteAsync(errorReportId);
                Console.WriteLine($"Ticket {errorReportId} has been deleted.\n");
            }
            else
            {
                Console.WriteLine($"Ticket {errorReportId} was not deleted.\n");
            }
        }
        else
        {
            Console.WriteLine($"Ticket {errorReportId} not found.");
        }
    }
}


