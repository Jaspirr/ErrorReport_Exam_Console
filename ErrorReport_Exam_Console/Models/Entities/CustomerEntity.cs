using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorReport_Exam_Console.Models.Entities
{
    [Index(nameof(EmailAdress), IsUnique = true)]
    public class CustomerEntity
    {
        [Key]
        public int CustomerId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [StringLength(100)]
        public string EmailAdress { get; set; } = null!;

        [Column(TypeName = "char(13)")]
        public string? PhoneNumber { get; set; }

        [Required]
        public int AddressId { get; set; }
        public WorkerEntity Worker { get; set; } = null!;

        public ICollection<ErrorReportEntity> Tickets { get; set; } = new HashSet<ErrorReportEntity>();

        public ICollection<CommentEntity> Comments { get; set; } = new HashSet<CommentEntity>();
    }
}
