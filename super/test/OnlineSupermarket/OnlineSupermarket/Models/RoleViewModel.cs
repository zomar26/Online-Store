using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OnlineSupermarket.Models
{
    public class RoleViewModel
    {
        [DisplayName("Role Name")]
        [RegularExpression("(Admin|User)" , ErrorMessage = "Role Must be Admin or User")]
        public string RoleName { get; set; }

    }
}
