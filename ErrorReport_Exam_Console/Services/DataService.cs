using ErrorReport_Exam_Console.Contexts;
using ErrorReport_Exam_Console.Models;
using ErrorReport_Exam_Console.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorReport_Exam_Console.Services
{
    internal class DataService
    {
        private static DataContext _context = new DataContext();

        public static async Task CreateAsync(ErrorReport errorReport)
        {
            var _errorReportEntity = new ErrorReportEntity
            {
                EmailAddress = errorReport.EmailAddress,
                Title = errorReport.Title,
                Description = errorReport.Description,
                ErrorReportStatus = errorReport.ErrorReportStatus,
                Time = DateTime.Now,
            };

            var _customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == errorReport.FirstName && x.LastName == errorReport.LastName && x.PhoneNumber == errorReport.PhoneNumber);
            if (_customerEntity != null)
            {
                _errorReportEntity.CustomerId = _customerEntity.CustomerId;
            }
            else
            {
                var newCustomer = new CustomerEntity
                {
                    FirstName = errorReport.FirstName,
                    LastName = errorReport.LastName,
                    PhoneNumber = errorReport.PhoneNumber
                };
                _context.Add(newCustomer);
                await _context.SaveChangesAsync();
                _errorReportEntity.CustomerId = newCustomer.CustomerId;
            }

            _context.Add(_errorReportEntity);
            await _context.SaveChangesAsync();
        }

        public static async Task<IEnumerable<ErrorReport>> GetAllAsync()
        {
            var _errorReports = await _context.ErrorReports
                .Include(x => x.Customer)
                .Select(x => new ErrorReport
                {
                    CustomerId = x.CustomerId,
                    FirstName = x.Customer.FirstName ?? "",
                    LastName = x.Customer.LastName ?? "",
                    EmailAddress = x.Customer.EmailAddress ?? "",
                    PhoneNumber = x.Customer.PhoneNumber ?? "",
                    Title = x.Title,
                    Description = x.Description,
                    Time = x.Time,
                    ErrorReportStatus = x.ErrorReportStatus
                })
                .ToListAsync();

            return _errorReports;
        }

        public static async Task<ErrorReportEntity> GetOneAsync(int id)
        {
            var errorReport = await _context.ErrorReports
            .Include(x => x.Customer)
            .Include(x => x.Comments)
            .SingleOrDefaultAsync(x => x.ErrorReportId == id);
            if (errorReport != null)
            {
                return errorReport;
            }
            else
                return null!;

        }

        public static async Task UpdateAsync(ErrorReportEntity errorReport)
        {
            var _errorReportEntity = await _context.ErrorReports.Include(x => x.Customer).FirstOrDefaultAsync(x => x.CustomerId == errorReport.CustomerId);
            if (_errorReportEntity != null)
            {
                if (!string.IsNullOrEmpty(_errorReportEntity.Title))
                    _errorReportEntity.Title = errorReport.Title;

                if (!string.IsNullOrEmpty(_errorReportEntity.Description))
                    _errorReportEntity.Description = errorReport.Description;

                if (!string.IsNullOrEmpty(_errorReportEntity.ErrorReportStatus))
                    _errorReportEntity.ErrorReportStatus = errorReport.ErrorReportStatus;

                if (!string.IsNullOrEmpty(errorReport.Customer.FirstName) || !string.IsNullOrEmpty(errorReport.Customer.LastName) || !string.IsNullOrEmpty(errorReport.Customer.EmailAddress))
                {
                    var _customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == errorReport.Customer.FirstName && x.LastName == errorReport.Customer.LastName && x.EmailAddress == errorReport.Customer.EmailAddress);
                    if (_customerEntity != null)
                        _errorReportEntity.CustomerId = _customerEntity.CustomerId;
                    else
                        _errorReportEntity.Customer = new CustomerEntity
                        {
                            FirstName = errorReport.Customer.FirstName,
                            LastName = errorReport.Customer.LastName,
                            EmailAddress = errorReport.Customer.EmailAddress,
                            PhoneNumber = errorReport.Customer.PhoneNumber
                        };
                }
                _context.Update(_errorReportEntity);
                await _context.SaveChangesAsync();
            }
        }
        public static async Task UpdateStatusAsync(ErrorReport errorReport)
        {
            var _errorReportEntity = await _context.ErrorReports.FirstOrDefaultAsync(x => errorReport.ErrorReportId == errorReport.ErrorReportId);
            if (_errorReportEntity != null)
            {


                _errorReportEntity.ErrorReportStatus = errorReport.ErrorReportStatus;

                _context.Update(_errorReportEntity);
                await _context.SaveChangesAsync();
            }
        }

        public static async Task DeleteAsync(int id)
        {
            var errorReport = await _context.ErrorReports.Include(x => x.Customer).FirstOrDefaultAsync(x => x.ErrorReportId == id);
            if (errorReport != null)
            {
                _context.Remove(errorReport);
                await _context.SaveChangesAsync();
            }
        }
        public static async Task AddCommentAsync(CommentModel entity)
        {
            var __errorReportEntity = await _context.ErrorReports.SingleOrDefaultAsync(x => x.ErrorReportId == entity.CommentId);
            if (__errorReportEntity != null)
            {
                var comment = new CommentEntity
                {
                    Comment = entity.Comment,
                    CreatedAt = entity.CreatedAt
                };
                __errorReportEntity.Comments.Add(comment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
