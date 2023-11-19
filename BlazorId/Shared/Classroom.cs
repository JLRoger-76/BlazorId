

using System.ComponentModel.DataAnnotations;

namespace BlazorId.Shared
{
    public class Classroom
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } 
        //userRole=Company
        public string UserId { get; set; }
        // Navigation property
        public ApplicationUser? User {  get; set; }
        
    }
}
