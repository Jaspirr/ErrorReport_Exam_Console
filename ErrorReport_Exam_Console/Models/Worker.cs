using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorReport_Exam_Console.Models
{
    public class Worker
    {
        public int WorkerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string NameInitials { get; set; } = null!;
    }
}
