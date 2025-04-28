using kek.data;
using kek.Entities;
using kek.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByIdAsync(userId!);
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
                        Ininiator = user.Email,
                        Status = 0,
                        TeamId = "null"
                    };

                    var result = _context.Add(idea);
                    if (result != null)
                    {
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Не удалось добавить идею");
                        return View(model);
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return View();

        }

        public async Task<IActionResult> ViewIdea(Idea _idea)
        {
            ViewBag.css = "~/css/site.css";
            return View(_idea);
        }
    }
}
