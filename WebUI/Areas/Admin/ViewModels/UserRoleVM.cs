using WebUI.Models;

namespace WebUI.Areas.Admin.ViewModels
{
    public class UserRoleVM
    {
        public AppUser AppUser { get; set; }
        public List<string> Roles { get; set; }
    }
}
