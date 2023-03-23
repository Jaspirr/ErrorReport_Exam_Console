using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorReport_Exam_Console.Models
{
    internal class CommentModel
    {
        public int CommentId { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime Time2 { get; set; }

    }
}
