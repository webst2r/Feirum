﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Feirum.Areas.Identity.Data;
using Feirum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;


namespace Feirum.Areas.Seller.Controllers
{
    [Area("Seller")]
    public class FairsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public FairsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Seller/Fairs
        [Route("/Seller/Fairs",
           Name = "Fairs")]
        public async Task<IActionResult> Index(string ownerId)
        {
            var user = await _userManager.GetUserAsync(User);
            var balance = user.Balance;
            ViewBag.userBalance = balance;
            ViewBag.OwnerId = _userManager.GetUserId(HttpContext.User);
            return View(await _context.Fairs.ToListAsync());
        }

        [Route("/Seller/Details",
           Name = "FairDetails")]
        // GET: Seller/Fairs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            var balance = user.Balance;
            ViewBag.userBalance = balance;
            if (id == null || _context.Fairs == null)
            {
                return NotFound();
            }

            var fairs = await _context.Fairs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fairs == null)
            {
                return NotFound();
            }

            return View(fairs);
        }

        // GET: Seller/Fairs/Create
        [Route("/Seller/Create",
           Name = "CreateFair")]
        public async Task<IActionResult> CreateAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var balance = user.Balance;
            ViewBag.userid = _userManager.GetUserId(HttpContext.User);
            return View();
        }

        // POST: Seller/Fairs/Create
        [Route("/Seller/Create",
           Name = "CreateFair")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Description,Email,Phone, Image")] CreateFairViewModel model)
        {
            ViewBag.userid = _userManager.GetUserId(HttpContext.User);

            if (ModelState.IsValid)
            {
                Fairs fairs = new Fairs();
                fairs.OwnerId = _userManager.GetUserId(HttpContext.User);
                fairs.CategoryId = model.CategoryId;
                fairs.Description = model.Description;
                fairs.Email = model.Email;
                fairs.Phone = model.Phone;
                fairs.Image = model.Image;

                _context.Add(fairs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Seller/Fairs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fairs == null)
            {
                return NotFound();
            }

            var fairs = await _context.Fairs.FindAsync(id);
            if (fairs == null)
            {
                return NotFound();
            }
            return View(fairs);
        }

        // POST: Seller/Fairs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OwnerId,CategoryId,Description,Email,Phone, Image")] Fairs fairs)
        {
            if (id != fairs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fairs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FairsExists(fairs.Id))
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
            return View(fairs);
        }

        // GET: Seller/Fairs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fairs == null)
            {
                return NotFound();
            }

            var fairs = await _context.Fairs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fairs == null)
            {
                return NotFound();
            }

            return View(fairs);
        }

        // POST: Seller/Fairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fairs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Fairs'  is null.");
            }
            var fairs = await _context.Fairs.FindAsync(id);
            if (fairs != null)
            {
                _context.Fairs.Remove(fairs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FairsExists(int id)
        {
          return _context.Fairs.Any(e => e.Id == id);
        }
    }
}
