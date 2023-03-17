using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorReport_Exam_Console.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; } = null!;
        public DateTime Time { get; set; }
        public int CustomerId { get; set; }
        public int ErrorReportId { get; set; }
    }
}
