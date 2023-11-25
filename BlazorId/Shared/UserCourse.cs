using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorId.Shared
{
    public class UserCourse
    {
        [Key]
        public int Id { get; set; }
        // Foreign Key
        public int CourseId { get; set; }
        // Foreign Key
        public string UserName { get; set; }
        public int Welcome { get; set; } = 0;
        public int Enviromnent { get; set; }=0;
        public int Content { get; set; } = 0;
        public int TeacherPedagogy { get; set; } = 0;
        public int TeacherExpertise { get; set; } = 0;
        public int TeacherAvailability { get; set; } = 0;
        public int TeacherAnswers { get; set; } = 0;
        public int TeacherAnimation { get; set; } = 0;
        public string Satisfaction { get; set; } = "";
        public bool Recommendation { get; set; } = false;
        public string CourseProject { get; set; } = "";
        // Navigation property
        public Course? Course { get; set; }
        // Navigation property
        public ICollection<DailyAttendance> DailyAttendance { get; } = new List<DailyAttendance>();
        [NotMapped]
        public bool IsPresent { get; set; } = false;
    }
}
