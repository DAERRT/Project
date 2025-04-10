using kek.data;
using kek.Entities;
using kek.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace kek.Controllers
{
    public class IdeasController : Controller
    {
        private readonly MyAppDbContext _context;
        private readonly UserManager<User> _userManager;

        public IdeasController(MyAppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddingIdeaViewModel model)
        {
            var user = _userManager?.GetUserAsync(User);
            if (user != null)
            {
                Idea idea = new Idea()
                {
                    IdeaName = model.IdeaName,
                    Problem = model.Problem,
                    Solution = model.Solution,
                    ExpectedResult = model.ExpectedResult,
                    NecessaryResourses = model.NecessaryResourses,
                    Stack = model.Stack,
                    Customer = model.Customer,
                    Ininiator = "lol@gmail.com",
                    Status = 0,
                    TeamId = null!
                };

                var result = _context.Add(idea);
                if (result != null)
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Какая-то хуйня");
                    return View(model);
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
    }
}
