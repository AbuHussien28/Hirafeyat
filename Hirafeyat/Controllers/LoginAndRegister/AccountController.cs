
using Hirafeyat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hirafeyat.Controllers.LoginAndRegister
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        #region Register
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userFromDb = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    FullName = model.FullName,
                    PasswordHash = model.Password,
                    ProfileImage = model.imagePath,
                    //brand_name= model.BrandName
                };
                IdentityResult result = await userManager.CreateAsync(userFromDb, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userFromDb, model.Role);
                    await signInManager.SignInAsync(userFromDb, false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("Register", model);
        }
        #endregion
        #region Logout
        public async Task<IActionResult> LogOut() 
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        #endregion
        #region Login
        public IActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model) 
        {
            if (ModelState.IsValid) 
            {
                ApplicationUser userFromDB=await userManager.FindByNameAsync(model.UserName);
                if (userFromDB != null) 
                {
                    bool found=await userManager.CheckPasswordAsync(userFromDB, model.Password);
                    if (found) 
                    {
                        await signInManager.SignInAsync(userFromDB, model.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                   
                }
                ModelState.AddModelError("", "Invalid Password");
            }
            return View("Login", model);
        }
        #endregion
    }
}
