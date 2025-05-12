using AspNetCoreIdentityApp.Web.Extenisons;
using AspNetCoreIdentityApp.Repository.Models;
using AspNetCoreIdentityApp.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreIdentityApp.Core.Models;
using AspNetCoreIdentityApp.Service.Services;


namespace AspNetCoreIdentityApp.Web.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {

        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFileProvider _fileProvider;
        private string userName => User.Identity!.Name!;
        private readonly IMemberService _memberService;


        public MemberController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IFileProvider fileProvider = null!, IMemberService memberService = null!)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _fileProvider = fileProvider;
            _memberService = memberService;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _memberService.GetUserViewModelByUserNameAsync(userName));
        }


        public async Task Logout()
        {
           await _memberService.LogoutAsync(); 

        }

        public IActionResult PasswordChange()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }



            if (! await _memberService.CheckPasswordAsync(userName,request.PasswordOld))
            {
                ModelState.AddModelError(string.Empty, "Eski şifre yanlış");
            }

            var(isSuccess,errros) = await _memberService.ChangePasswordAsync(userName, request.PasswordOld, request.PasswordNew);

            if (!isSuccess)
            {
                ModelState.AddModelErrorList(errros!);
                return View();
            }

 
            TempData["SuccessMessage"] = "Şifreniz başarıyla değiştirilmiştir";


            return View();
        }


        public async Task<IActionResult> UserEdit()
        {
            ViewBag.genderList = _memberService.GetGenderSelectList();

            return View(await _memberService.GetUserEditViewModelAsync(userName));
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            var(isSuccess,errors) = await _memberService.EditUserAsync(request, userName);


            if (!isSuccess)
            {
                ModelState.AddModelErrorList(errors!);
                return View();
            }


            TempData["SuccessMessage"] = "Üye bilgileri başarıyla değiştirilmiştir";


            return View(await _memberService.GetUserEditViewModelAsync(userName));

        }

        [HttpGet]
        public IActionResult Claims()
        {

            return View(_memberService.GetClaims(User));
        }

        [Authorize(Policy = "AnkaraPolicy")]
        [HttpGet]
        public IActionResult AnkaraPage()
        {
            return View();
        }

        [Authorize(Policy = "TekirdagPolicy")]
        [HttpGet]
        public IActionResult TekirdagPage()
        {
            return View();
        }

        [Authorize(Policy = "IstanbulPolicy")]
        [HttpGet]
        public IActionResult IstanbulPage()
        {
            return View();
        }


        [Authorize(Policy = "ExchangePolicy")]
        [HttpGet]
        public IActionResult ExchangePage()
        {
            return View();
        }

        
        [Authorize(Policy = "ViolencePolicy")]
        [HttpGet]
        public IActionResult ViolencePage()
        {
            return View();
        }


        public IActionResult AccessDenied(string ReturnUrl)
        {

            string message = string.Empty;

            ViewBag.message = "Bu sayfayı görmeye yetkiniz yoktur!";

            return View();
        }




    }
}
