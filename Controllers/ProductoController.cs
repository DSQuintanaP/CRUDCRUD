using Microsoft.AspNetCore.Mvc;
using CRUDCRUD.Data;
using CRUDCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDCRUD.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _AppDbcontext;
        public ProductoController(ApplicationDbContext AppDbcontext)
        {
            _AppDbcontext = AppDbcontext;
        }
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Producto> lista = await _AppDbcontext.Productos.ToListAsync();
            return View(lista);
        }
        [HttpGet]
        public  IActionResult NewProduct()
        {
            
            return View();
        }
    }
}
