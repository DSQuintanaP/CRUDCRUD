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

        // GET: Compras
        public async Task<IActionResult> Index()
        {
            var compras = await _context.compras
                .Include(c => c.customerName) // Incluir la relación con Cliente
                .Include(c => c.DetalleCompras)
                    .ThenInclude(d => d.Producto) // Incluir los detalles de la compra y los productos
                .ToListAsync();

            return View(compras);
        }

        // GET: Compras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.compras
                .Include(c => c.customerName)
                .Include(c => c.DetalleCompras)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(m => m.IDOrder == id);

            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }
        // GET: Compras/Create
        public IActionResult Create()
        {
            var compra = new Compras
            {
                DetalleCompras = new List<DetalleCompra> { new DetalleCompra() } // Inicializamos la lista con un detalle vacío
            };

            ViewData["IDCustomer"] = new SelectList(_context.clientes, "IDCustomer", "customerName"); // Lista de clientes
            ViewData["Productos"] = new SelectList(_context.Productos, "prductID", "productName"); // Lista de productos para los detalles
            return View(compra); // Pasamos el objeto compra inicializado a la vista
        }

        // POST: Compras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IDOrder,IDCustomer,fecha,totalValue,DetalleCompras")] Compras compra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compra);
                await _context.SaveChangesAsync();

                // Guardar detalles de la compra
                foreach (var detalle in compra.DetalleCompras)
                {
                    detalle.IDOrder = compra.IDOrder; // Asignar el IDOrder a los detalles
                    _context.Add(detalle);
                    _context.Add(detalle);
                    _context.detallecompra.Count().Equals(1);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IDCustomer"] = new SelectList(_context.clientes, "IDCustomer", "customerName", compra.IDCustomer);
            ViewData["prductID"] = new SelectList(_context.Productos, "prductID", "productName");
            return View(compra);
        }



        //// GET: Compras/Create
        //public IActionResult Create()
        //{
        //    ViewData["IDCustomer"] = new SelectList(_context.clientes, "IDCustomer", "CustomerName"); // Lista de clientes
        //    ViewData["Productos"] = new SelectList(_context.Productos, "prductID", "productName"); // Lista de productos para los detalles
        //    return View();
        //}

        //// POST: Compras/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("IDOrder,IDCustomer,fecha,Cantidad,totalValue,DetalleCompras")] Compras compra)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(compra);
        //        await _context.SaveChangesAsync();

        //        // Guardar detalles de la compra
        //        foreach (var detalle in compra.DetalleCompras)
        //        {
        //            detalle.IDOrder = compra.IDOrder; // Asignar el IDOrder a los detalles
        //            _context.Add(detalle);
        //        }

        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["IDCustomer"] = new SelectList(_context.clientes, "IDCustomer", "CustomerName", compra.IDCustomer);
        //    ViewData["Productos"] = new SelectList(_context.Productos, "prductID", "productName");
        //    return View(compra);
        //}

        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.compras
                .Include(c => c.DetalleCompras) // Incluimos los detalles
                .FirstOrDefaultAsync(m => m.IDOrder == id);

            if (compra == null)
            {
                return NotFound();
            }

            // Lista de clientes y productos para los dropdowns
            ViewData["IDCustomer"] = new SelectList(_context.clientes, "IDCustomer", "customerName", compra.IDCustomer);
            ViewData["Productos"] = new SelectList(_context.Productos, "prductID", "productName");
            return View(compra);
        }

        // POST: Compras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IDOrder,IDCustomer,fecha,Cantidad,totalValue,DetalleCompras")] Compras compra)
        {
            if (id != compra.IDOrder)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizamos la compra
                    _context.Update(compra);
                    await _context.SaveChangesAsync();

                    // Obtener detalles originales de la base de datos
                    var detallesOriginales = await _context.detallecompra
                        .Where(d => d.IDOrder == id)
                        .ToListAsync();

                    // Eliminar detalles que ya no están en la compra editada
                    foreach (var detalleOriginal in detallesOriginales)
                    {
                        if (!compra.DetalleCompras.Any(d => d.prductID == detalleOriginal.prductID))
                        {
                            _context.detallecompra.Remove(detalleOriginal);
                        }
                    }

                    // Actualizar o añadir nuevos detalles
                    foreach (var detalle in compra.DetalleCompras)
                    {
                        var detalleExistente = detallesOriginales
                            .FirstOrDefault(d => d.prductID == detalle.prductID);

                        if (detalleExistente != null)
                        {
                            // Actualizamos el detalle existente
                            detalleExistente.Cantidad = detalle.Cantidad;
                            detalleExistente.Precio = detalle.Precio;
                            _context.Update(detalleExistente);
                        }
                        else
                        {
                            // Añadimos un nuevo detalle
                            detalle.IDOrder = compra.IDOrder;
                            _context.detallecompra.Add(detalle);
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComprasExists(compra.IDOrder))
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

            // Si hay un error, volvemos a cargar las listas desplegables y mostramos la vista nuevamente
            ViewData["IDCustomer"] = new SelectList(_context.clientes, "IDCustomer", "customerName", compra.IDCustomer);
            ViewData["Productos"] = new SelectList(_context.Productos, "prductID", "productName");
            return View(compra);
        }
        
        //// GET: Compras/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var compra = await _context.compras
        //        .Include(c => c.DetalleCompras)
        //        .FirstOrDefaultAsync(m => m.IDOrder == id);

        //    if (compra == null)
        //    {
        //        return NotFound();
        //    }

        //    ViewData["IDCustomer"] = new SelectList(_context.clientes, "IDCustomer", "customerName", compra.IDCustomer);
        //    ViewData["Productos"] = new SelectList(_context.Productos, "prductID", "productName");
        //    return View(compra);
        //}

        //// POST: Compras/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("IDOrder,IDCustomer,fecha,Cantidad,totalValue,DetalleCompras")] Compras compra)
        //{
        //    if (id != compra.IDOrder)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(compra);

        //            // Actualizar detalles de la compra
        //            foreach (var detalle in compra.DetalleCompras)
        //            {
        //                detalle.IDOrder = compra.IDOrder;
        //                _context.Update(detalle);
        //            }

        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ComprasExists(compra.IDOrder))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["IDCustomer"] = new SelectList(_context.clientes, "IDCustomer", "customerName", compra.IDCustomer);
        //    ViewData["Productos"] = new SelectList(_context.Productos, "prductID", "productName");
        //    return View(compra);
        //}

        // GET: Compras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.compras
                .Include(c => c.customerName)
                .Include(c => c.DetalleCompras)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(m => m.IDOrder == id);

            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compra = await _context.compras
                .Include(c => c.DetalleCompras)
                .FirstOrDefaultAsync(m => m.IDOrder == id);

            if (compra != null)
            {
                // Eliminar detalles de la compra primero
                foreach (var detalle in compra.DetalleCompras)
                {
                    _context.detallecompra.Remove(detalle);
                }

                _context.compras.Remove(compra);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ComprasExists(int id)
        {
            return _context.compras.Any(e => e.IDOrder == id);
        }
    }
}













//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using CRUDCRUD.Data;
//using CRUDCRUD.Models;

//namespace CRUDCRUD.Controllers
//{
//    public class ComprasController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public ComprasController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        //// GET: Compras
//        //public async Task<IActionResult> Index()
//        //{
//        //    var applicationDbContext = _context.compras.Include(c => c.customerName).Include(c => c.DetalleCompras);
//        //    return View(await applicationDbContext.ToListAsync());
//        //}

//        // GET: Compras
//        public async Task<IActionResult> Index()
//        {
//            var applicationDbContext = _context.compras.Include(c => c.customerName).Include(c => c.DetalleCompras);
//            return View(await applicationDbContext.ToListAsync());
//        }


//        // GET: Compras/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var compras = await _context.compras
//                .Include(c => c.customerName)
//                .Include(c => c.DetalleCompras)
//                .FirstOrDefaultAsync(m => m.IDOrder == id);
//            if (compras == null)
//            {
//                return NotFound();
//            }

//            return View(compras);
//        }

//        // GET: Compras/Create
//        public IActionResult Create()
//        {
//            ViewData["IDCustomer"] = new SelectList(_context.clientes, "IDCustomer", "customerName");
//            return View();
//        }

//        // POST: Compras/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(Compras compras, List<DetalleCompra> detalleCompras)
//        {
//            if (ModelState.IsValid)
//            {
//                compras.DetalleCompras = detalleCompras; // Asignar los detalles a la compra
//                _context.Add(compras);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["IDCustomer"] = new SelectList(_context.clientes, "IDCustomer", "customerName", compras.IDCustomer);
//            return View(compras);
//        }

//        // GET: Compras/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var compras = await _context.compras
//                .Include(c => c.DetalleCompras)
//                .FirstOrDefaultAsync(c => c.IDOrder == id);
//            if (compras == null)
//            {
//                return NotFound();
//            }

//            ViewData["IDCustomer"] = new SelectList(_context.clientes, "IDCustomer", "customerName", compras.IDCustomer);
//            return View(compras);
//        }

//        // POST: Compras/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, Compras compras)
//        {
//            if (id != compras.IDOrder)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(compras);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!ComprasExists(compras.IDOrder))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["IDCustomer"] = new SelectList(_context.clientes, "IDCustomer", "customerName", compras.IDCustomer);
//            return View(compras);
//        }

//        // GET: Compras/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var compras = await _context.compras
//                .Include(c => c.customerName)
//                .Include(c => c.DetalleCompras)
//                .FirstOrDefaultAsync(m => m.IDOrder == id);
//            if (compras == null)
//            {
//                return NotFound();
//            }

//            return View(compras);
//        }

//        // POST: Compras/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var compras = await _context.compras.FindAsync(id);
//            if (compras != null)
//            {
//                _context.compras.Remove(compras);
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool ComprasExists(int id)
//        {
//            return _context.compras.Any(e => e.IDOrder == id);
//        }
//    }
//}
