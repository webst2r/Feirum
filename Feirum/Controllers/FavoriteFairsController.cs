﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Feirum.Areas.Identity.Data;
using Feirum.Models;

namespace Feirum.Controllers
{
    public class FavoriteFairsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FavoriteFairsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FavoriteFairs
        public async Task<IActionResult> Index()
        {
              return View(await _context.FavoriteFair.ToListAsync());
        }

        // GET: FavoriteFairs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.FavoriteFair == null)
            {
                return NotFound();
            }

            var favoriteFair = await _context.FavoriteFair
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (favoriteFair == null)
            {
                return NotFound();
            }

            return View(favoriteFair);
        }

        // GET: FavoriteFairs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FavoriteFairs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FairId")] FavoriteFair favoriteFair)
        {
            if (ModelState.IsValid)
            {
                _context.Add(favoriteFair);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(favoriteFair);
        }

        // GET: FavoriteFairs/Edit/5
        public async Task<IActionResult> Edit(string? userId , int? fairId)
        {
            if (userId == null || _context.FavoriteFair == null)
            {
                return NotFound();
            }

            var favoriteFair = await _context.FavoriteFair.FindAsync(userId, fairId);
            if (favoriteFair == null)
            {
                return NotFound();
            }
            return View(favoriteFair);
        }

        // POST: FavoriteFairs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,FairId")] FavoriteFair favoriteFair)
        {
            if (id != favoriteFair.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favoriteFair);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavoriteFairExists(favoriteFair.UserId))
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
            return View(favoriteFair);
        }

        // GET: FavoriteFairs/Delete/5
        public async Task<IActionResult> Delete(string? userId, int? fairId)
        {
            if (userId == null || _context.FavoriteFair == null)
            {
                return NotFound();
            }

            var favoriteFair = await _context.FavoriteFair.FindAsync(userId, fairId);
            if (favoriteFair == null)
            {
                return NotFound();
            }

            return View(favoriteFair);
        }

        // POST: FavoriteFairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string? userId, int? fairId)
        {
            if (_context.FavoriteFair == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FavoriteFair'  is null.");
            }
            var favoriteFair = await _context.FavoriteFair.FindAsync(userId, fairId);
            if (favoriteFair != null)
            {
                _context.FavoriteFair.Remove(favoriteFair);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavoriteFairExists(string id)
        {
          return _context.FavoriteFair.Any(e => e.UserId == id);
        }
    }
}
