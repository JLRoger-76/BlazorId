using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorId.Shared
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public int TrainingId { get; set; }
        public int ClassroomId {  get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
        public DateTime EndDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        // Navigation property
        public Training? Training { get; set; }
        // Navigation property
        public Classroom? Classroom { get; set; }
    }
}
