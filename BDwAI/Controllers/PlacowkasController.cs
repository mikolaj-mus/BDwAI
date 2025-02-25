﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BDwAI.Data;
using BDwAI.Models;

namespace BDwAI.Controllers
{
    public class PlacowkasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlacowkasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Placowkas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Placowka.ToListAsync());
        }

        // GET: Placowkas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placowka = await _context.Placowka
                .FirstOrDefaultAsync(m => m.Id == id);
            if (placowka == null)
            {
                return NotFound();
            }

            return View(placowka);
        }

        // GET: Placowkas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Placowkas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Miasto,Ocena,Opis")] Placowka placowka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(placowka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(placowka);
        }

        // GET: Placowkas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placowka = await _context.Placowka.FindAsync(id);
            if (placowka == null)
            {
                return NotFound();
            }
            return View(placowka);
        }

        // POST: Placowkas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Miasto,Ocena,Opis")] Placowka placowka)
        {
            if (id != placowka.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(placowka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlacowkaExists(placowka.Id))
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
            return View(placowka);
        }

        // GET: Placowkas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placowka = await _context.Placowka
                .FirstOrDefaultAsync(m => m.Id == id);
            if (placowka == null)
            {
                return NotFound();
            }

            return View(placowka);
        }

        // POST: Placowkas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var placowka = await _context.Placowka.FindAsync(id);
            if (placowka != null)
            {
                _context.Placowka.Remove(placowka);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlacowkaExists(int id)
        {
            return _context.Placowka.Any(e => e.Id == id);
        }
    }
}
