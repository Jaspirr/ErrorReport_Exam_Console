using ErrorReport_Exam_Console.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorReport_Exam_Console.Models
{
    internal class ErrorReport
    {

        public int ErrorReportId { get; set; }
        public Guid CustomerId { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Time { get; set; } = DateTime.Now;
        public string Comment { get; set; } = null!;
        public string ErrorReportStatus { get; set; } = null!;
    }
}
