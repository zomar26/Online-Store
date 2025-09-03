using System.ComponentModel.DataAnnotations;

namespace OnlineSupermarket.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is requried")]
        public string Name { get; set; } 
    }
}
