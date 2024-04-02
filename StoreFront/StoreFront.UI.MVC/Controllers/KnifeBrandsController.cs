using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;
using StoreFront.UI.MVC.Utilities;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;

namespace StoreFront.UI.MVC.Controllers
{
    public class KnifeBrandsController : Controller
    {
        private readonly StorefrontContext _context;

        public KnifeBrandsController(StorefrontContext context)
        {
            _context = context;
        }

        // GET: KnifeBrands
        public async Task<IActionResult> Index()
        {
              return _context.KnifeBrands != null ? 
                          View(await _context.KnifeBrands.ToListAsync()) :
                          Problem("Entity set 'StorefrontContext.KnifeBrands'  is null.");
        }

        // GET: KnifeBrands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.KnifeBrands == null)
            {
                return NotFound();
            }

            var knifeBrand = await _context.KnifeBrands
                .FirstOrDefaultAsync(m => m.BrandId == id);
            if (knifeBrand == null)
            {
                return NotFound();
            }

            return View(knifeBrand);
        }

        // GET: KnifeBrands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KnifeBrands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrandId,BrandName,Address,City,State,Zip,Phone")] KnifeBrand knifeBrand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(knifeBrand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(knifeBrand);
        }

        // GET: KnifeBrands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.KnifeBrands == null)
            {
                return NotFound();
            }

            var knifeBrand = await _context.KnifeBrands.FindAsync(id);
            if (knifeBrand == null)
            {
                return NotFound();
            }
            return View(knifeBrand);
        }

        // POST: KnifeBrands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BrandId,BrandName,Address,City,State,Zip,Phone")] KnifeBrand knifeBrand)
        {
            if (id != knifeBrand.BrandId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(knifeBrand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnifeBrandExists(knifeBrand.BrandId))
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
            return View(knifeBrand);
        }

        // GET: KnifeBrands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.KnifeBrands == null)
            {
                return NotFound();
            }

            var knifeBrand = await _context.KnifeBrands
                .FirstOrDefaultAsync(m => m.BrandId == id);
            if (knifeBrand == null)
            {
                return NotFound();
            }

            return View(knifeBrand);
        }

        // POST: KnifeBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.KnifeBrands == null)
            {
                return Problem("Entity set 'StorefrontContext.KnifeBrands'  is null.");
            }
            var knifeBrand = await _context.KnifeBrands.FindAsync(id);
            if (knifeBrand != null)
            {
                _context.KnifeBrands.Remove(knifeBrand);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KnifeBrandExists(int id)
        {
          return (_context.KnifeBrands?.Any(e => e.BrandId == id)).GetValueOrDefault();
        }
    }
}
