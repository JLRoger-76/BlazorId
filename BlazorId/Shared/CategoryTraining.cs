using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorId.Shared
{
    public class CategoryTraining
    {
        [Key]
        public int Id { get; set; }
        // Foreign Key
        public int CategoryId { get; set; }
        // Foreign Key
        public int TrainingId { get; set; }
        // Navigation property
        public Category? Category { get; set; }
        // Navigation property
        public Training? Training { get; set; }
    }
}
