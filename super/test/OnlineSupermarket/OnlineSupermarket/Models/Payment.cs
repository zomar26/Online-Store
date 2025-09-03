using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSupermarket.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Payment method is required")]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; } 

        [Required(ErrorMessage = "Payment date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Payment Date")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Card number is required")]
        [CreditCard(ErrorMessage = "Invalid card number")]
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Card holder name is required")]
        [Display(Name = "Card Holder Name")]
        public string CardHolderName { get; set; } //= string.Empty;

        [Required(ErrorMessage = "Expiry date is required")]
        [Display(Name = "Expiry Date")]
        public string ExpiryDate { get; set; } //= string.Empty;

        [Required(ErrorMessage = "CVV is required")]
        [Display(Name = "CVV")]
        public string CVV { get; set; } = string.Empty;

       
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }
    }
}
