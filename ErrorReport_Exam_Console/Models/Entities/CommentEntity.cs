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
        public int CustomerId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(500)")]
        public string Text { get; set; } = null!;

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public int ErrorReportId { get; set; }
        public ErrorReportEntity ErrorReport { get; set; } = null!;
    }
}
