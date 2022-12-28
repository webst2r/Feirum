using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Feirum.Areas.Identity.Data;
using Feirum.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using Microsoft.CodeAnalysis;
using static NuGet.Packaging.PackagingConstants;
using NuGet.Versioning;

namespace Feirum.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        [Route("/Orders",
           Name = "Orders")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var balance = user.Balance;
            ViewBag.userBalance = balance;
            ViewBag.BuyerId = _userManager.GetUserId(HttpContext.User);
            return View(await _context.Orders.ToListAsync());
        }

        [Route("/Orders/Details",
           Name = "OrderDetails")]
        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        [Route("/Orders/Create",
           Name = "OrdersCreate")]
        // GET: Orders/Create
        public async Task<IActionResult> Create(int productId)
        {
            var user = await _userManager.GetUserAsync(User);
            var balance = user.Balance;
            ViewBag.userBalance = balance;
            List<Products> list = await (from productItem in _context.Products
                                        where productItem.Id == productId
                                        select new Products
                                        {
                                            Id = productItem.Id,
                                            Description = productItem.Description,
                                            Quantity = productItem.Quantity,
                                            UnitPrice = productItem.UnitPrice,
                                            Image = productItem.Image,
                                            FairId = productItem.FairId
                                        }).ToListAsync();

            var product = list.First();

            if (product != null)
            {
                var model = new AddOrderViewModel
                {
                    ProductId = productId,
                    UnitPrice = product.UnitPrice,
                    Quantity = 1,
                };
                return View(model);
            }
            
            ViewBag.ProductId = productId;
            ViewBag.BuyerId = _userManager.GetUserId(HttpContext.User);
            return NotFound();
        }

        [Route("/Orders/Create",
           Name = "OrdersCreate")]
        // POST: Orders/Create
        //[Bind("Id,BuyerId,ProductId,Quantity,TotalPrice,Date")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int productId, AddOrderViewModel model)
        {
            ViewBag.ProductId = productId;
            ViewBag.BuyerId = _userManager.GetUserId(HttpContext.User);

            var buyerId = _userManager.GetUserId(HttpContext.User);   
            var product = _context.Products.Find(model.ProductId);


            if (product != null)
            {
                var user = await _userManager.GetUserAsync(User);
                var userBalance = user.Balance;
                var totalCost = model.Quantity * product.UnitPrice;

                if (userBalance < totalCost)
                {
                    return View("OrderError");
                } else
                {
                    
                    var newBalance = userBalance - totalCost;
                    user.Balance = newBalance;

                    var order = new Orders();
                    order.ProductId = product.Id;
                    order.BuyerId = buyerId;
                    order.Quantity = model.Quantity;
                    order.TotalPrice = model.Quantity * product.UnitPrice;
                    order.Date = DateTime.Now;

                    _context.Add(order);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            return View("OrderError");
        }


        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BuyerId,ProductId,Quantity,TotalPrice,Date")] Orders orders)
        {
            if (id != orders.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Order'  is null.");
            }
            var orders = await _context.Orders.FindAsync(id);
            if (orders != null)
            {
                _context.Orders.Remove(orders);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
          return _context.Orders.Any(e => e.Id == id);
        }
    }
}
