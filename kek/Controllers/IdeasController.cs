using kek.data;
using kek.Entities;
using kek.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
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
            User user = await _userManager.GetUserAsync(User);
            ViewBag.userEmail = user.Email;
            var userRole = await _userManager.GetRolesAsync(user);
            ViewBag.role = userRole[0];
            return View(_idea);
        }

        [HttpPost]
        public IActionResult Myau(string ideaId)
        {
            return RedirectToAction("EditIdea", new { id =  ideaId});
        }

        public async Task<IActionResult> EditIdea(string id)
        {
            Idea idea = await _context.Ideas.FindAsync(id);
            EditIdeaViewModel model = new EditIdeaViewModel
            {
                Id = idea.Id,
                IdeaName = idea.IdeaName,
                Problem = idea.Problem,
                Solution = idea.Solution,
                ExpectedResult = idea.ExpectedResult,
                NecessaryResourses = idea.NecessaryResourses,
                Stack = idea.Stack,
                Customer = idea.Customer
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditIdea(EditIdeaViewModel model)
        {
            
            if (ModelState.IsValid && model.Id != null)
            {
                Idea idea = await _context.Ideas.FindAsync(model.Id);
                if (idea != null)
                {
                    idea.IdeaName = model.IdeaName;
                    idea.Problem = model.Problem;
                    idea.Solution = model.Solution;
                    idea.ExpectedResult = model.ExpectedResult;
                    idea.NecessaryResourses = model.NecessaryResourses;
                    idea.Stack = model.Stack;
                    idea.Customer = model.Customer;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Возникла ошибка :(");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string ideaId)
        {
            var idea = await _context.Ideas.FindAsync(ideaId);
            if (idea == null)
            {
                return NotFound();
            }

            _context.Ideas.Remove(idea);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
