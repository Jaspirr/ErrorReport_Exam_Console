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

        [Required]
        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; } = null!;

        [Required]
        public ErrorReportStatus Status { get; set; }

        public ICollection<CommentEntity> Comments { get; set; } = new HashSet<CommentEntity>();

        [Required]
        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }

    public enum ErrorReportStatus
    {
        NotStarted,
        Started,
        Closed
    }
}
