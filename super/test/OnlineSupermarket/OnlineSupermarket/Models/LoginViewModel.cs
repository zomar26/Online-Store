using System.ComponentModel.DataAnnotations;

namespace OnlineSupermarket.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "يرجى إدخال البريد الإلكتروني أو رقم الهاتف")]
        [Display(Name = "البريد الإلكتروني أو رقم الهاتف")]
        public string EmailOrPhone { get; set; }

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تذكرني")]
        public bool RememberMe { get; set; }
    }
}
