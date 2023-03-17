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
    public class WorkerEntity
    {
        [Key]
        public int WorkerId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string EmailAdress { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string NameInitials { get; set; } = null!;


        public ICollection<CustomerEntity> Customers = new HashSet<CustomerEntity>();

    }
}
