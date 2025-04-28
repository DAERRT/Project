using kek.data;
using kek.Entities;
using kek.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Security.Claims;

namespace kek.Controllers
{
    public class TeamsController : Controller
    {
        private readonly MyAppDbContext _context;
        private readonly UserManager<User> _usermanager;

        public TeamsController(MyAppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _usermanager = userManager;
        }

        public async Task<IActionResult> Teams()
        {
            var user = await _usermanager.GetUserAsync(User);
            ViewBag.role = await _usermanager.GetRolesAsync(user);
            var _teams = await _context.Teams.ToListAsync();
            return View(_teams);
        }

        [HttpGet]
        public IActionResult TeamCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TeamCreate(TeamCreationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _usermanager.FindByIdAsync(userId!);
                if (user != null)
                {
                    Teams newTeam = new Teams()
                    {
                        TeamName = model.TeamName,
                        TeamDescription = model.TeamDescription,
                        TeamMembers = [user.Email],
                        TeamCreator = user.Email,
                        TeamLead = "Невыбран",
                        IdeaId = "null"
                    };
                    var result = await _context.Teams.AddAsync(newTeam);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Teams", "Teams");
                }
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public async Task<IActionResult> TeamView(Teams Team)
        {
            Teams team = await _context.Teams.FindAsync(Team.Id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _usermanager.FindByIdAsync(userId!);
            if (user.Email == team.TeamCreator)
            {
                return RedirectToAction("TeamEdit", "Teams", new { id = Team.Id });
            }
            return View(team);
        }

        public async Task<IActionResult> TeamEdit(string id)
        {
            Teams Team = await _context.Teams.FindAsync(id);
            var _allUsers = await _usermanager.Users.ToListAsync();
            var allUsers = new List<User>();
            if (Team != null)
            {
                foreach (var user in _allUsers)
                {
                    if (!Team.TeamMembers.Contains(user.Email))
                    {
                        allUsers.Add(user);
                    }
                }
            }
            else
            {
                return RedirectToAction("Teams");
            }
            TeamEditViewModel model = new TeamEditViewModel()
            {
                Users = allUsers,
                Id = Team.Id,
                TeamName = Team.TeamName,
                TeamLead = Team.TeamLead,
                TeamCreator = Team.TeamCreator,
                TeamDescription = Team.TeamDescription,
                TeamMembers = Team.TeamMembers,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> TeamEdit(string teamid, List<string> userIds)
        {
            Teams team = await _context.Teams.FindAsync(teamid);
            if (team != null && userIds != null)
            {
                var _usersToAdd = await _usermanager.Users
                                .Where(u => userIds.Contains(u.Id))
                                .ToListAsync();
                var usersToAdd = new List<string>();
                foreach (var user in _usersToAdd)
                {
                    usersToAdd.Add(user.Email);
                }       
                team.TeamMembers.AddRange(usersToAdd);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("TeamEdit",new {id = teamid});
        }

        [HttpPost]
        public async Task<IActionResult> AddTeamLead(string teamid, string teamlead)
        {
            Teams team = await _context.Teams.FindAsync(teamid);
            if (teamlead != null) 
            {
                team.TeamLead = teamlead;
            }
            else
            {
                team.TeamLead = "Не выбран";
            }
                _context.SaveChangesAsync();
            return RedirectToAction("TeamEdit", new {id = teamid});
        }

    }
}
