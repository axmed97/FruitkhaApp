using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Areas.Admin.ViewModels;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    // primary constructor
    [Area(nameof(Admin))]
    public class UserController(
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager) : Controller
    {
        public IActionResult Index()
        {
            var users = userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> AddRole(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            List<string> roles = roleManager.Roles.Select(x => x.Name).ToList();
            var userroles = await userManager.GetRolesAsync(user);

            UserRoleVM userRoleVM = new()
            {
                AppUser = user,
                Roles = roles.Except(userroles).ToList()
            };

            return View(userRoleVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string userId, string role)
        {
            AppUser user = await userManager.FindByIdAsync(userId);
            await userManager.AddToRoleAsync(user, role);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return View(user);
        }

        public async Task<IActionResult> Delete(string userId, string role)
        {
            var user = await userManager.FindByIdAsync(userId);
            await userManager.RemoveFromRoleAsync(user, role);
            return RedirectToAction("Index");
        }
    }
}
