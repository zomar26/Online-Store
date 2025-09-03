using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineSupermarket.Models;

namespace OnlineSupermarket.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel rolefromrequest)
        {
            IdentityRole role = new IdentityRole();
            role.Name = rolefromrequest.RoleName;
            IdentityResult result = await roleManager.CreateAsync(role);
            if (ModelState.IsValid)
            {
                if (result.Succeeded)
                {
                   
                    return RedirectToAction("Index" , "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("AddRole", rolefromrequest);
        }

    }
}
