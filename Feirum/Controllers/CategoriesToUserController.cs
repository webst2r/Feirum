using Feirum.Areas.Identity.Data;
using Feirum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Feirum.Controllers
{
    public class CategoriesToUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public CategoriesToUserController(ApplicationDbContext context, UserManager<User> userManager) 
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var balance = user.Balance;
            ViewBag.userBalance = balance;

            CategoriesToUserModel categoriesToUserModel= new CategoriesToUserModel();
            var userId = _userManager.GetUserAsync(User).Result?.Id;

            categoriesToUserModel.Categories = await GetCategoriesThatHaveFairs();
            categoriesToUserModel.UserId = (int)userId;
            
            return View(categoriesToUserModel);
        }

        private async Task<List<Categories>> GetCategoriesThatHaveFairs()
        {
            var categoriesThatHaveFairs = await (from categories in _context.Categories
                                                 join fairs in _context.Fairs
                                                 on categories.Id equals fairs.CategoryId
                                                 join products in _context.Products
                                                 on fairs.Id equals products.FairId
                                                 select new Categories
                                                 {
                                                     Id = categories.Id,
                                                     Description= categories.Description,
                                                     Image = categories.Image
                                                 }).Distinct().ToListAsync();
            return categoriesThatHaveFairs;
        }

    }
}
