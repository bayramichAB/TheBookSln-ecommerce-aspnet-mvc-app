using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TheBookshelf.Models;
using TheBookshelf.Models.ViewModels;

namespace TheBookshelf.Controllers
{
    public class AccountController:Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private AppIdentityDbContext appIdentityDbContext;

        public AccountController(UserManager<AppUser> userMgr,SignInManager<AppUser> signInMgr, AppIdentityDbContext appIdyDbContext)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            appIdentityDbContext = appIdyDbContext;
        }


        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var registerModel = new RegisterModel()
                {
                  FullName = user.FullName,
                  Email= user.Email
                };
                //return View(user);
                return View(registerModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerModel);
            }

            //old user
            var  user = await userManager.FindByIdAsync(registerModel.Id);
            if (user != null)
            {         //new Email            //old Email
                if (registerModel.Email != user.Email)
                {
                    var findUserInDb = await userManager.FindByEmailAsync(registerModel.Email);
                    if (findUserInDb != null)
                    {
                        TempData["Error"] = "This email address is already in use";
                        return View(registerModel);
                    }
                }

                user.FullName = registerModel.FullName;
                user.Email = registerModel.Email;
                user.UserName = registerModel.Email;

                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded && !String.IsNullOrEmpty(registerModel.Password))
                {
                    await userManager.RemovePasswordAsync(user);
                    result = await userManager.AddPasswordAsync(user, registerModel.Password);
                }
                if (result.Succeeded)
                {
                    return RedirectToAction("Index","Home");
                }
                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("",err.Description);
                }
            }
            return View(registerModel);
        }



        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Users()
        {
            var users = await appIdentityDbContext.Users.ToListAsync();
            return View(users);
        }

        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {

            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            var user = await userManager.FindByEmailAsync(loginModel.Email);
            if (user!=null)
            {
                var passwordChek = await userManager.CheckPasswordAsync(user,loginModel.Password);
                if (passwordChek)
                {
                    //await signInManager.SignOutAsync();
                    var result = await signInManager.PasswordSignInAsync(user,loginModel.Password,false,false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Invalid name or password");
                return View(loginModel);
            }
            ModelState.AddModelError("", "Invalid name or password");
            return View(loginModel);
        }

        public IActionResult Register() => View(new RegisterModel());


        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerModel);
            }

            var user = await userManager.FindByEmailAsync(registerModel.Email);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerModel);
            }

            var newUser = new AppUser()
            {
                FullName = registerModel.FullName,
                Email = registerModel.Email,
                UserName = registerModel.Email
            };

            var result = await userManager.CreateAsync(newUser,registerModel.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            return View("RegisterCompleted");
        }

        
        public async Task<RedirectResult> Logout(string returnUrl="/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }

}
