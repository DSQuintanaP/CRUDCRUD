using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDCRUD.Data;
using CRUDCRUD.Models;

namespace CRUDCRUD.Controllers
{
    public class ComprasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComprasController(ApplicationDbContext context)
        {
            _context = context;
        }

        //// GET: Compras
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.compras.Include(c => c.customerName).Include(c => c.DetalleCompras);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        // GET: Compras
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.compras.Include(c => c.customerName).Include(c => c.DetalleCompras);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Compras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compras = await _context.compras
                .Include(c => c.customerName)
                .Include(c => c.DetalleCompras)
                .FirstOrDefaultAsync(m => m.IDOrder == id);
            if (compras == null)
            {
                return NotFound();
            }

            return View(compras);
        }

        // GET: Compras/Create
        public IActionResult Create()
        {
            ViewData["IDCustomer"] = new SelectList(_context.clientes, "IDCustomer", "Nombre");
            return View();
        }

        // POST: Compras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Compras compras, List<DetalleCompra> detalleCompras)
        {
            if (ModelState.IsValid)
            {
                compras.DetalleCompras = detalleCompras; // Asignar los detalles a la compra
                _context.Add(compras);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IDCustomer"] = new SelectList(_context.clientes, "IDCustomer", "Nombre", compras.IDCustomer);
            return View(compras);
        }

        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compras = await _context.compras
                .Include(c => c.DetalleCompras)
                .FirstOrDefaultAsync(c => c.IDOrder == id);
            if (compras == null)
            {
                return NotFound();
            }

            ViewData["IDCustomer"] = new SelectList(_context.clientes, "IDCustomer", "Nombre", compras.IDCustomer);
            return View(compras);
        }

        // POST: Compras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Compras compras)
        {
            if (id != compras.IDOrder)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compras);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComprasExists(compras.IDOrder))
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
            ViewData["IDCustomer"] = new SelectList(_context.clientes, "IDCustomer", "Nombre", compras.IDCustomer);
            return View(compras);
        }

        // GET: Compras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compras = await _context.compras
                .Include(c => c.customerName)
                .Include(c => c.DetalleCompras)
                .FirstOrDefaultAsync(m => m.IDOrder == id);
            if (compras == null)
            {
                return NotFound();
            }

            return View(compras);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compras = await _context.compras.FindAsync(id);
            if (compras != null)
            {
                _context.compras.Remove(compras);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComprasExists(int id)
        {
            return _context.compras.Any(e => e.IDOrder == id);
        }
    }
}
