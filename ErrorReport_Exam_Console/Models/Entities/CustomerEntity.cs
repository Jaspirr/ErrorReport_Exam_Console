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
    [Index(nameof(EmailAddress), IsUnique = true)]
    public class CustomerEntity
    {
        [Key]
        public int CustomerId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [StringLength(100)]
        public string EmailAddress { get; set; } = null!;

        [Column(TypeName = "char(13)")]
        public string PhoneNumber { get; set; } = null!;
        public virtual ICollection<ErrorReportEntity> ErrorReports { get; set; } = new List<ErrorReportEntity>();
    }
}
