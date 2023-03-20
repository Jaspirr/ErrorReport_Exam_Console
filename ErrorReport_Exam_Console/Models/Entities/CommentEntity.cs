using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorReport_Exam_Console.Models.Entities
{
    public class CommentEntity
    {
        [Key]
        public int CommentId { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public int ErrorReportId { get; set; }
        public ErrorReportEntity ErrorReports { get; set; } = null!;
    }
}
