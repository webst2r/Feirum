using Feirum.Areas.Identity.Data;
using Feirum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Feirum.Controllers
{
    public class UserFairsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserFairsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index(int fairId)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = await _userManager.GetUserIdAsync(user);
            var fair = _context.Fairs.Find(fairId);
            var balance = user.Balance;
            ViewBag.fairId = fair.Id;
            ViewBag.userBalance = balance;
            ViewBag.userId = userId;
            ViewBag.fairName = fair.Description;
            ViewBag.fairImage = fair.Image;
            ViewBag.categoryId = fair.CategoryId;
            

            var favoriteFair = _context.FavoriteFair.Find(userId, fairId);
            if(favoriteFair == null)
            {
                ViewBag.isFavorite = false;
            } else { ViewBag.isFavorite = true; }
            

            List<Products> list = await (from productItem in _context.Products
                                         where productItem.FairId == fairId
                                         select new Products
                                         {
                                             Id = productItem.Id,
                                             Description = productItem.Description,
                                             Quantity = productItem.Quantity,
                                             UnitPrice = productItem.UnitPrice,
                                             Image = productItem.Image,
                                             FairId = fairId
                                         }).ToListAsync();

            ViewBag.FairId = fairId;
            return View(list);
        }


    }
}
