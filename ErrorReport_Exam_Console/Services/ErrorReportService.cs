using ErrorReport_Exam_Console.Contexts;
using ErrorReport_Exam_Console.Models.Entities;
using ErrorReport_Exam_Console.Models;
using ErrorReport_Exam_Console.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ErrorReport_Exam_Console.Services
{
    public class ErrorReportService
    {
        private static DataContext _context = new DataContext();


        public static async Task SaveChangesAsync(ErrorReport errorReport, Customer customer)
        {
            var _workerEntity = new WorkerEntity
            {
                FirstName = errorReport.FirstName,
                LastName = errorReport.LastName,
                NameInitials = errorReport.NameInitials
            };

            _context.Add(_workerEntity);
            await _context.SaveChangesAsync();

            var _customerEntity = new CustomerEntity
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                EmailAddress = customer.EmailAddress,
                PhoneNumber = customer.PhoneNumber,
                LastName = _WorkerEntity.WorkerId,

            };

            _context.Add(_customerEntity);
            await _context.SaveChangesAsync();

            var _errorReportEntity = new ErrorReportEntity
            {
                Description = errorReport.Description,
                ErrorReportStatus = errorReport.ErrorReportStatus,
                CustomerId = _customerEntity.CustomerId,
                CreatedAt = errorReport.CreatedAt,
                UpdatedAt = errorReport.UpdatedAt,
                Comments = new List<CommentEntity>()
            };

            _context.Add(_errorReportEntity);
            await _context.SaveChangesAsync();

            // Save comments for the ticket
            foreach (var comment in errorReport.Comments)
            {
                var _commentEntity = new CommentEntity
                {
                    Text = comment.Text,
                    Time = comment.Time,
                    ErrorReportId = _errorReportEntity.ErrorReportId,

                };

                _context.Add(_commentEntity);

                // Associate the comment entity with the ticket entity
                _errorReportEntity.Comments.Add(_commentEntity);

                // Save the comment to the CommentEntity if it doesn't exist
                if (_context.Comments.FirstOrDefault(c => c.ErrorReportId == _commentEntity.ErrorReportId) == null)
                {
                    _context.Comments.Add(_commentEntity);
                    await _context.SaveChangesAsync();
                }
            }

            await _context.SaveChangesAsync();
        }



        public static async Task<IEnumerable<ErrorReport>> GetAllAsync()
        {
            var _errorReports = new List<ErrorReport>();

            foreach (var _errorReport in await _context.ErrorReports.Include(x => x.Comments).Include(x => x.Customer).ToListAsync())
                _errorReports.Add(new ErrorReport
                {
                    CustomerId = _errorReport.Id,
                    Description = _errorReport.Description,
                    ErrorReportStatus = Enum.Parse<ErrorReportStatus>(_errorReport.Status.ToString()),
                    CustomerId = _errorReport.CustomerId,
                    CreatedAt = _errorReport.CreatedAt,
                    UpdatedAt = _errorReport.UpdatedAt,
                    Customer = new Customer
                    {
                        CustomerId = _errorReport.Customer.Id,
                        FirstName = _errorReport.Customer.FirstName,
                        LastName = _errorReport.Customer.LastName,
                        EmailAddress = _errorReport.Customer.Email,
                        PhoneNumber = _errorReport.Customer.PhoneNumber
                    },
                    Comments = _errorReport.Comments.Select(x => new Comment
                    {
                        CommentId = x.Id,
                        Text = x.Text,
                        Time = x.Timestamp,
                        ErrorReportId = x.TicketId,
                    }).ToList()
                });

            return _errorReports;
        }


        public static async Task<ErrorReport> GetAsync(int id)
        {
            var _errorReport = await _context.ErrorReports
                .Include(x => x.Comments)
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (_errorReport != null)
            {
                return new ErrorReport
                {
                    ErrorReportId = _errorReport.Id,
                    Description = _errorReport.Description,
                    ErrorReportStatus = (ErrorReportStatus)_errorReport.Status,
                    CustomerId = _errorReport.CustomerId,
                    CreatedAt = _errorReport.CreatedAt,
                    UpdatedAt = _errorReport.UpdatedAt,
                    Customer = new Customer
                    {
                        CustomerId = _errorReport.Customer.Id,
                        FirstName = _errorReport.Customer.FirstName,
                        LastName = _errorReport.Customer.LastName,
                        EmailAddress = _errorReport.Customer.Email,
                        PhoneNumber = _errorReport.Customer.PhoneNumber
                    },
                    Comments = _errorReport.Comments.Select(x => new Comment
                    {
                        CommentId = x.Id,
                        Text = x.Text,
                        Time = x.Time,
                        ErrorReportId = x.ErrorReportId,
                    }).ToList()
                };
            }
            else
            {
                return null!;
            }
        }



        public static async Task UpdateAsync(ErrorReport errorReport)
        {

            // Get the ticket entity from the database, including its comments
            var _errorReportEntity = await _context.ErrorReports.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == ticket.Id);

            // Check if the ticket exists
            if (_errorReportEntity != null)
            {
                // Update the ticket properties if they're not null or default
                if (!string.IsNullOrEmpty(errorReport.Description))
                    _errorReportEntity.Description = errorReport.Description;

                if (errorReport.Status != ErrorReportStatus.NotStarted)
                    _errorReportEntity.Status = errorReport.ErrorReportStatus;

                if (errorReport.CustomerId != 0)
                    _errorReportEntity.CustomerId = errorReport.CustomerId;

                if (errorReport.UpdatedAt != DateTime.MinValue)
                    _errorReportEntity.UpdatedAt = errorReport.UpdatedAt;

                // Update comments for the ticket
                foreach (var comment in errorReport.Comments)
                {
                    // Get the comment entity from the database
                    var _commentEntity = await _context.Comments.FirstOrDefaultAsync(x => x.Id == comment.Id);

                    // Check if the comment exists
                    if (_commentEntity != null)
                    {
                        // Update the comment properties if they're not null or default
                        if (!string.IsNullOrEmpty(comment.Text))
                            _commentEntity.Text = comment.Text;

                        if (comment.Time != DateTime.MinValue)
                            _commentEntity.Time = comment.Time;

                        // Mark the comment as modified in the context
                        _context.Update(_commentEntity);
                    }
                    else
                    {
                        // If the comment doesn't exist, create a new comment entity
                        _commentEntity = new CommentEntity
                        {
                            Text = comment.Text,
                            Time = DateTime.Now,
                            ErrorReportId = _errorReportEntity.Id
                        };

                        // Add the comment entity to the context
                        _context.Comments.Add(_commentEntity);
                    }
                }

                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
        }


        public static async Task DeleteAsync(int errorReportId)
        {
            var errorReport = await _context.ErrorReports
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == ticketId);

            if (errorReport != null)
            {
                _context.ErrorReports.Remove(errorReport);
                await _context.SaveChangesAsync();
            }
        }
    }
}

