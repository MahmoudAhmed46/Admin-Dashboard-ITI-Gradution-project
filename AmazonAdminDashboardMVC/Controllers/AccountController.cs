using AmazonAdmin.Application.Services;
using AmazonAdmin.Domain;
using AmazonAdmin.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AmazonAdminDashboardMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IUserService _UserService;
        private readonly IMapper _Mapper;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        public AccountController(UserManager<ApplicationUser> userManager,IUserService userService,IMapper mapper,SignInManager<ApplicationUser> signInManager)
        {
            _UserManager = userManager;
            _UserService = userService;
            _Mapper = mapper;
            _SignInManager = signInManager;
        }

        

        public async Task<IActionResult> Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(UserRegisterDTO user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationuser = _Mapper.Map<ApplicationUser>(user);
                IdentityResult result=await _UserManager.CreateAsync(applicationuser,user.Password);
                if (result.Succeeded)
                {
                    try
                    {
						await _UserManager.AddToRoleAsync(applicationuser, "Admin");
					}catch(Exception ex)
                    {
						ModelState.AddModelError("", "Error On Adding Role");
					}
                   await _SignInManager.SignInAsync(applicationuser, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }
            return View(user);
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginDTO user)
        {
            if (ModelState.IsValid)
            {
               ApplicationUser usermodel= await _UserManager.FindByNameAsync(user.userName);
                if(usermodel != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult res =await _SignInManager.PasswordSignInAsync(usermodel, user.Password,user.RememberMe,false);
                    if (res.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wrong User Name Or Password !");
                }
            }
            return View(user);
        }
        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync(); 
            return RedirectToAction("Index", "Home");
        }
    }
}
