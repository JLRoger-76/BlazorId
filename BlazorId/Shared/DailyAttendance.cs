using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BlazorId.Shared
{
    public class DailyAttendance
    {
        [Key]
        public int Id { get; set; }
        // Foreign Key
        public int UserCourseId { get; set; } // Required foreign key property
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
        public DateTime DailyDate { get; set; }
        public bool IsPresent { get; set; }
        // Navigation Property
        public UserCourse? UserCourse { get; set; } = null!;
    }
}
