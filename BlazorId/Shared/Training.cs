using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorId.Shared
{
    public class Training
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public string? Detail { get; set; }
    }
}
