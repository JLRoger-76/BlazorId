using System.ComponentModel.DataAnnotations;

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
        public int? Welcome { get; set; } 
        public int? Enviromnent { get; set; }
        public int? Content { get; set; }
        public int? TeacherPedagogy { get; set; }
        public int? TeacherExpertise { get; set; }
        public int? TeacherAvailability { get; set; }
        public int? TeacherAnswers {  get; set; }
        public int? TeacherAnimation { get; set; }
        public string? Satisfaction { get; set; }
        public bool? Recommendation { get; set; }
        public string? CourseProject { get; set; }
    }
}
