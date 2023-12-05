using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaAb104.DAL;
using ProniaAb104.Models;

namespace ProniaAb104.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]

    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        //private readonly IWebHostBuilder _env;
        public ProductController(AppDbContext context)
        {
            _context = context;
            //_env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages.Where(pi=>pi.IsPrimary==true))
                .ToListAsync();
           
            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid) return BadRequest();
            return View();
        }
    }
}
