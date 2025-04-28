using Microsoft.AspNetCore.Mvc;
using kek.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using kek.Entities;

namespace kek.Controllers;

public class HomeController : Controller
{
    private readonly MyAppDbContext _context;
    private readonly UserManager<User> _userManager;

    public HomeController(MyAppDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        ViewBag.role = await _userManager.GetRolesAsync(user);
        var _ideas = await _context.Ideas.ToListAsync();
        return View(_ideas);
    }
}
