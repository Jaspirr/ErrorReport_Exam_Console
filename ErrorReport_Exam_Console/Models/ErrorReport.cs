using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorReport_Exam_Console.Models
{
    public class ErrorReport
    {
        public int ErrorReportId { get; set; }
        public string Description { get; set; } = null!;
        public string? ErrorReportStatus { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public Customer Customer { get; set; } = null!;

    }
}
