using Feirum.Areas.Identity.Data;
using Feirum.Models;
using MessagePack;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Feirum.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public CategoriesController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(int categoryId)
        {
            var user = await _userManager.GetUserAsync(User);
            var balance = user.Balance;
            ViewBag.userBalance = balance;

            Categories category = new Categories();
            var categoryFairs = await (from fairs in _context.Fairs
                                       join categories in _context.Categories
                                       on fairs.CategoryId equals categoryId
                                       select new Fairs
                                       {
                                           Id = fairs.Id,
                                           OwnerId = fairs.OwnerId,
                                           CategoryId = categoryId,
                                           Description = fairs.Description,
                                           State = fairs.State,
                                           Email = fairs.Email,
                                           Phone = fairs.Phone,
                                           Image = fairs.Image

                                       }).Distinct().ToListAsync();
            category.FairsList = categoryFairs;
            ViewBag.Categoryid = categoryId;

            return View(category);
        }
    }
}
