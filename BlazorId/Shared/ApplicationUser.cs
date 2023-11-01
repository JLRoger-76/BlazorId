using Microsoft.AspNetCore.Identity;

namespace BlazorId.Shared
{
    public class ApplicationUser : IdentityUser
    {
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

    }
    
}