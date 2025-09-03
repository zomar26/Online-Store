using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineSupermarket.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required (ErrorMessage ="يرجي كتابة اسم العميل ")]
        [Display(Name = "اسم العميل")]
        public string CustomerName { get; set; }

        [Required (ErrorMessage ="يرجي كتابة رقم الهاتف ")]
        [Phone]
        [Display(Name = "رقم الهاتف")]
        public string PhoneNumber { get; set; }

        [Display(Name = "العنوان")]
        public string Address { get; set; }

        [Display(Name = "توصيل")]
        public bool IsDelivery { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
