using ErrorReport_Exam_Console.Contexts;
using ErrorReport_Exam_Console.Models.Entities;
using ErrorReport_Exam_Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorReport_Exam_Console.Services;

public class CommentService
{
    private static DataContext _context = new DataContext();

    public static async Task AddCommentAsync(CommentEntity comment)
    {
        /*var ticket = await TicketService.GetAsync(ticketId);
        if (ticket == null)
        {
            throw new InvalidOperationException("Ticket not found.");
        } */

        _context.Add(comment);
        await _context.SaveChangesAsync();
        //await TicketService.UpdateAsync(ticket); 

    }

    public static async Task<List<Comment>> GetAllCommentsAsync(int errorReportId)
    {
        var errorReport = await TicketService.GetAsync(errorReportId);

        if (errorReport == null)
        {
            throw new InvalidOperationException("Ticket not found.");
        }

        return errorReport.Comments.ToList();


        public static async Task<Comment?> GetCommentByIdAsync(int errorReportId, int commentId)
        {
            var errorReport = await ErrorReportService.GetAsync(errorReportId);

            if (errorReport == null)
            {
                throw new InvalidOperationException("Ticket not found.");
            }

            return errorReport.Comments.FirstOrDefault(c => c.Id == commentId);
        }

        public static async Task UpdateCommentAsync(int errorReportId, Comment comment)
        {
            var errorReport = await TicketService.GetAsync(errorReportId);

            if (errorReport == null)
            {
                throw new InvalidOperationException("Ticket not found.");
            }

            var existingComment = errorReport.Comments.FirstOrDefault(c => c.Id == comment.Id);

            if (existingComment == null)
            {
                throw new InvalidOperationException("Comment not found.");
            }

            existingComment.Text = comment.Text;
            existingComment.Time = comment.Time;

            await ErrorReportService.UpdateAsync(errorReport);
        }

        public static async Task DeleteCommentAsync(int errorReportId, int commentId)
        {
            var errorReport = await ErrorReportService.GetAsync(errorReportId);

            if (errorReport == null)
            {
                throw new InvalidOperationException("Ticket not found.");
            }

            var commentToRemove = errorReport.Comments.FirstOrDefault(c => c.Id == commentId);

            if (commentToRemove == null)
            {
                throw new InvalidOperationException("Comment not found.");
            }

            errorReport.Comments.Remove(commentToRemove);
            await TicketService.UpdateAsync(errorReport);
        }

        public static async Task AddCommentToTicketAsync(string text)
        {
            var comment = new CommentEntity()
            {
                Text = text,
                Time = DateTime.UtcNow
            };

            await AddCommentAsync(comment);
        }
    }
}
