using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSupermarket.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required (ErrorMessage ="يرجي كتابة اسم المنتج")]
        public string Name { get; set; } 

        [Required (ErrorMessage ="يرجي كتابة سعر المنتج")]
        [Column(TypeName = "decimal(18,2)")] 
        public decimal Price { get; set; }

        public string Description { get; set; } 

        public string? ImageUrl { get; set; } 
        [NotMapped]
        public IFormFile Image { get; set; }


        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
