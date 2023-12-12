using Mamba.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mamba.MVC.Areas.Manage.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    [Area("manage")]
    public class DashBoardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DashBoardController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser admin = new AppUser()
        //    {
        //        FullName = "Tenzile Abdiyeva",
        //        UserName = "SuperAdmin"     

        //    };
        //    var result = await _userManager.CreateAsync(admin, "Admin1234@");

        //    return Ok(result);
        //}
        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role1 = new IdentityRole("SuperAdmin");
        //    IdentityRole role2 = new IdentityRole("Admin");
        //    IdentityRole role3 = new IdentityRole("Member");

        //    await _roleManager.CreateAsync(role1); 
        //    await _roleManager.CreateAsync(role2);
        //    await _roleManager.CreateAsync(role3);
        //    return Ok("created");
        //}
        //public async Task<IActionResult> AddRoleAdmin()
        //{
        //    AppUser admin = await _userManager.FindByNameAsync("SuperAdmin");
        //    await _userManager.AddToRoleAsync(admin, "Admin");
        //    return Ok("Add olundu");

        //}

    }
}
