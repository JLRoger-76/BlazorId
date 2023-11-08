using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorId.Shared
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }

        [NotMapped]
        public bool ExistChildren { get; set; }
        [NotMapped]
        public bool IsLinked {  get; set; }
    }
}
