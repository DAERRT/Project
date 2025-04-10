using Microsoft.AspNetCore.Mvc;
using kek.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace kek.Controllers;

public class HomeController : Controller
{
    private readonly MyAppDbContext _context;

    public HomeController(MyAppDbContext context)
    {
        _context = context;
    }

    [Authorize(Roles ="Student")]
    public async Task<IActionResult> Index()
    {
        var _ideas = await _context.Ideas.ToListAsync();
        return View(_ideas);
    }
}
