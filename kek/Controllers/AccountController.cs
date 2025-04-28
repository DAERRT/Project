using kek.data;
using kek.Entities;
using kek.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.SymbolStore;
using System.Security.Claims;

namespace kek.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly MyAppDbContext _context;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, MyAppDbContext context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this._context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email!, model.Password!, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неверный Email или Пароль");
                    return View(model);
                }
            }
            return View(model);
        }


        public IActionResult Reg()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Reg(RegViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email,
                    StudyGroup = model.StudyGroup,
                    Status = 0
                };

                var result = await userManager.CreateAsync(user, model.Password!);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Студент");
                    await signInManager.PasswordSignInAsync(model.Email!, model.Password!, true, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View(model);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var resetPasswordResult = await userManager.RemovePasswordAsync(user);
                    if (resetPasswordResult.Succeeded)
                    {
                        var addPasswordResult = await userManager.AddPasswordAsync(user, model.NewPassword);
                        if (addPasswordResult.Succeeded)
                        {
                            // Пароль изменен успешно
                            return RedirectToAction("Login");
                        }
                        else
                        {
                            // Обработка ошибок при добавлении нового пароля
                            foreach (var error in addPasswordResult.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                    }
                    else
                    {
                        // Обработка ошибок при удалении старого пароля
                        foreach (var error in resetPasswordResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден.");
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> PersonalAccount()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            var role = await userManager.GetRolesAsync(user);
            ViewBag.role = role;
            var _teams = await _context.Teams.ToListAsync();
            var Teams = new List<Teams>();
            Teams = _teams.Where(team => team.TeamMembers.Contains(user.Email)).ToList();
            var _ideas = await _context.Ideas.ToListAsync();
            var Ideas = new List<Idea>();
            var userteamids = Teams.Select(team => team.Id).ToList();
            Ideas = _ideas.Where(idea => userteamids.Contains(idea.TeamId)).ToList();
            var model = new PersonalAccountViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName, 
                LastName = user.LastName,
                UserName = user.UserName,
                Teams = Teams,
                Ideas = Ideas,
            };
            return View(model);
        }

        public async Task<IActionResult> ViewUserAccountData(string userEmail)
        {
            var user = await userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return NotFound();
            }
            var role = await userManager.GetRolesAsync(user);
            ViewBag.role = role;
            var model = new User
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName
            };
            return View(model);
        }

        //public IActionResult ChangeInfo()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> ChangeInfo(User model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        userManager.Change
        //    }
        //    return View();
        //}

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            var users = await _context.Users
                .Where(u => u.Email.Contains(query))
                .Select(u => new { id = u.Id, text = u.Email })
                .Take(10)
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersByIds([FromQuery] List<string> ids)
        {
            var users = await _context.Users
                .Where(u => ids.Contains(u.Id))
                .Select(u => new { id = u.Id, email = u.Email })
                .ToListAsync();

            return Ok(users);
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
