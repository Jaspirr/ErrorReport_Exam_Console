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
    public class ErrorReportEntity
    {
        [Key]
        public int ErrorReportId { get; set; }

        [StringLength(100)]
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        [StringLength(30)]
        public string? ErrorReportStatus { get; set; }
        public DateTime Time { get; set; }

        public Guid CustomerId { get; set; } = Guid.NewGuid();
        public CustomerEntity Customer { get; set; } = null!;
        public virtual ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
        public string EmailAddress { get; set; } = null!;
    }
        public enum ErrorReportStatus
    {
        NotStarted,
        Open,
        Closed
    }
}
