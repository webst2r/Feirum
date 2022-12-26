using Feirum.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Feirum.Models;

namespace Feirum.Controllers
{
    public class WalletController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public WalletController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> IndexAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.userBalance = user.Balance;
            ViewBag.userid = user.Id;
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Deposit(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.userBalance = user.Balance;
            var uid = _userManager.GetUserId(User);
            ViewBag.userid = uid;

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User cannot be found";
                return View("NotFound");
            }

            var model = new EditUserBalanceViewModel
            {
                Amount = user.Balance
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(EditUserBalanceViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.userBalance = user.Balance;

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User cannot be found";
                return View("NotFound");
            }
            else
            {
                var oldBalance = user.Balance;
                var newBalance = oldBalance + model.Amount;
                user.Balance = newBalance;
               
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Withdraw(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.userBalance = user.Balance;
            var uid = _userManager.GetUserId(User);
            ViewBag.userid = uid;

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User cannot be found";
                return View("NotFound");
            }

            var model = new EditUserBalanceViewModel
            {
                Amount = user.Balance
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Withdraw(EditUserBalanceViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.userBalance = user.Balance;

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User cannot be found";
                return View("NotFound");
            }
            else
            {
                var oldBalance = user.Balance;


                var newBalance = oldBalance - model.Amount;

                if(newBalance < 0)
                {
                    ViewBag.ErrorMessage = $"Não pode levantar essa quantia.";
                    return View("WithdrawError");
                } else
                {
                    user.Balance = newBalance;
                }

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

    }
}
