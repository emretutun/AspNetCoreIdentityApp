using AspNetCoreIdentityApp.Core.Models;
using AspNetCoreIdentityApp.Web.Areas.Admin.Models;
using AspNetCoreIdentityApp.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentityApp.Web.Areas.Admin.Controllers
{
    [Authorize(Roles ="admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager; // Değişken adı düzeltildi

        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager; // Doğru atama yapıldı
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UserList()
        {
            var userList = await _userManager.Users.ToListAsync(); // ToList yerine ToListAsync() kullanıldı.

            var userViewModelList = userList.Select(x => new UserViewModel()
            {
                Id = x.Id,
                Email = x.Email,
                Name = x.UserName
            }).ToList();

            return View(userViewModelList); // Kullanıcı listesini View'e gönderdik.
        }
    }
}
