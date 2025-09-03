using System.ComponentModel.DataAnnotations;

namespace OnlineSupermarket.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "الاسم الأول مطلوب")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "الاسم الأخير مطلوب")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "يرجى إدخال بريد إلكتروني أو رقم هاتف")]
        [Display(Name = "البريد الإلكتروني أو رقم الهاتف")]
        public string EmailOrPhone { get; set; }

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "كلمة المرور يجب أن تكون بين 7 و 50 حرفًا")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "يرجى تأكيد كلمة المرور")]
        [Compare("Password", ErrorMessage = "كلمتا المرور غير متطابقتين")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
