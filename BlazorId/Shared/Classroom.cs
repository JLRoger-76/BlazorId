

using System.ComponentModel.DataAnnotations;

namespace BlazorId.Shared
{
    public class Classroom
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } 
        //userRole=Center
        public string UserId { get; set; } 
        public ApplicationUser? User {  get; set; }
        
    }
}
