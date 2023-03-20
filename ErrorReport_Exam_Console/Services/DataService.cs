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
                _errorReportEntity.CustomerId = _customerEntity.CustomerId;
            else
                _errorReportEntity.Customer = new CustomerEntity
                {
                    FirstName = errorReport.FirstName,
                    LastName = errorReport.LastName,
                    PhoneNumber = errorReport.PhoneNumber
                };

            _context.Add(_errorReportEntity);
            await _context.SaveChangesAsync();
        }

        public static async Task<IEnumerable<ErrorReport>> GetAllAsync()
        {
            var _errorReports = new List<ErrorReport>();

            foreach (var _errorReport in await _context.ErrorReports.Include(x => x.Customer).ToListAsync())
                _errorReports.Add(new ErrorReport
                {
                    CustomerId = _errorReport.CustomerId,
                    FirstName = _errorReport.Customer.FirstName,
                    LastName = _errorReport.Customer.LastName,
                    EmailAddress = _errorReport.Customer.EmailAddress,
                    PhoneNumber = _errorReport.Customer.PhoneNumber,
                    Title = _errorReport.Title,
                    Description = _errorReport.Description,
                    Time = _errorReport.Time,
                    ErrorReportStatus = _errorReport.ErrorReportStatus
                });
            return _errorReports;
        }



        public static async Task<ErrorReport> GetOneAsync(int id)
        {
            var _errorReport = await _context.ErrorReports.Include(x => x.Customer).FirstOrDefaultAsync(x => x.ErrorReportId == id);
            if (_errorReport != null)
                return new ErrorReport
                {
                    ErrorReportId = _errorReport.ErrorReportId,
                    FirstName = _errorReport.Customer.FirstName,
                    LastName = _errorReport.Customer.LastName,
                    EmailAddress = _errorReport.Customer.EmailAddress,
                    PhoneNumber = _errorReport.Customer.PhoneNumber,
                    Title = _errorReport.Title,
                    Description = _errorReport.Description,
                    Time = _errorReport.Time,
                    ErrorReportStatus = _errorReport.ErrorReportStatus

                };
            else
                return null!;
        }

        public static async Task UpdateAsync(ErrorReport errorReport)
        {
            var _errorReportEntity = await _context.ErrorReports.Include(x => x.Customer).FirstOrDefaultAsync(x => x.ErrorReportId == errorReport.ErrorReportId);
            if (_errorReportEntity != null)
            {

                if (!string.IsNullOrEmpty(errorReport.EmailAddress))
                    _errorReportEntity.EmailAddress = errorReport.EmailAddress;

                if (!string.IsNullOrEmpty(errorReport.Title))
                    _errorReportEntity.Title = errorReport.Title;

                if (!string.IsNullOrEmpty(errorReport.Description))
                    _errorReportEntity.Description = errorReport.Description;

                if (!string.IsNullOrEmpty(errorReport.FirstName) || !string.IsNullOrEmpty(errorReport.LastName) || !string.IsNullOrEmpty(errorReport.EmailAddress) || !string.IsNullOrEmpty(errorReport.PhoneNumber))
                {
                    var _customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == errorReport.FirstName && x.LastName == errorReport.LastName && x.PhoneNumber == errorReport.PhoneNumber);
                    if (_customerEntity != null)
                        _errorReportEntity.CustomerId = _customerEntity.CustomerId;
                    else
                        _errorReportEntity.Customer = new CustomerEntity
                        {
                            FirstName = errorReport.FirstName,
                            LastName = errorReport.LastName,
                            PhoneNumber = errorReport.PhoneNumber
                        };
                }

                _context.Update(_errorReportEntity);
                await _context.SaveChangesAsync();
            }
        }
        public static async Task UpdateStatusAsync(ErrorReport errorReport)
        {
            var __errorReportEntity = await _context.ErrorReports.FirstOrDefaultAsync(x => errorReport.ErrorReportId == errorReport.ErrorReportId);
            if (__errorReportEntity != null)
            {


                __errorReportEntity.ErrorReportStatus = errorReport.ErrorReportStatus;

                _context.Update(__errorReportEntity);
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
    }
}
