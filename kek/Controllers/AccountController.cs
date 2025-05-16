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
                User user = await userManager.FindByEmailAsync(model.Email);
                var userrole = await userManager.GetRolesAsync(user);
                if (result.Succeeded)
                {
                    if (userrole[0] == "Администратор")
                    {
                        return RedirectToAction("Admin", "Home");
                    }
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
            PersonalAccountViewModel model;
            if (user.PhoneNumber != null && user.PhoneNumber != string.Empty)
            {
                model = new PersonalAccountViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    Teams = Teams,
                    Ideas = Ideas,
                };
            }
            else
            {
                model = new PersonalAccountViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Teams = Teams,
                    Ideas = Ideas,
                };
            }
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

        [HttpGet]
        public async Task<IActionResult> ChangeInfo(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new ChangeInfoViewModel
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                StudyGroup = user.StudyGroup,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeInfo(ChangeInfoViewModel model)
        {

            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }

            // Обновляем основные данные пользователя
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> ChangeInfoAdmin(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await userManager.GetRolesAsync(user);
            var currentRole = userRoles.FirstOrDefault();

            var model = new ChangeInfoAdminViewModel
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                StudyGroup = user.StudyGroup,
                AvailableRoles = roleManager.Roles
                .Select(r => new RoleViewModel
                {
                    RoleId = r.Id,
                    RoleName = r.Name
                }).ToList(),
                SelectedRoleId = currentRole != null ?
                (await roleManager.FindByNameAsync(currentRole))?.Id : null
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeInfoAdmin(ChangeInfoAdminViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Перезагружаем доступные роли при ошибке валидации
                model.AvailableRoles = (await roleManager.Roles.ToListAsync())
                    .Select(r => new RoleViewModel
                    {
                        RoleId = r.Id,
                        RoleName = r.Name
                    }).ToList();
                return View(model);
            }

            if (string.IsNullOrEmpty(model.SelectedRoleId))
            {
                ModelState.AddModelError("SelectedRoleId", "Пожалуйста, выберите роль");
                model.AvailableRoles = (await roleManager.Roles.ToListAsync())
                    .Select(r => new RoleViewModel
                    {
                        RoleId = r.Id,
                        RoleName = r.Name
                    }).ToList();
                return View(model);
            }

            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }

            // Обновляем основные данные пользователя
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            var selectedRole = await roleManager.FindByIdAsync(model.SelectedRoleId);
            if (selectedRole != null)
            {
                // Удаляем все текущие роли
                var currentRoles = await userManager.GetRolesAsync(user);
                await userManager.RemoveFromRolesAsync(user, currentRoles);

                // Добавляем новую роль
                await userManager.AddToRoleAsync(user, selectedRole.Name);
            }

            return RedirectToAction("Users", "Home");
        }

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

        [HttpGet]
        public async Task<IActionResult> PersonalAccountForAdmin(string id)
        {
            var user = await userManager.FindByIdAsync(id);
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


        [HttpPost]
        [Authorize(Roles ="Администратор")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID '{id}' not found.");
            }

            // Дополнительная проверка (например, нельзя удалить себя)
            var currentUser = await userManager.GetUserAsync(User);
            if (user.Id == currentUser.Id)
            {
                return BadRequest("You cannot delete your own account this way.");
            }

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error deleting user: {string.Join(", ", result.Errors)}");
            }
            return RedirectToAction("Users", "Home");
        }
    }
}
