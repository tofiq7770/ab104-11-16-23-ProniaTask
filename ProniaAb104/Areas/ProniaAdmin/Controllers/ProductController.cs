using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaAb104.Areas.ProniaAdmin.ViewModels;
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
                .Include(p => p.ProductImages.Where(pi => pi.IsPrimary == true))
                .ToListAsync();

            return View(products);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM productVM)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View();
            }
            bool result = await _context.Categories.AnyAsync(c => c.Id == productVM.CategoryId);
            if (!result)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                ModelState.AddModelError("CategoryId", "Bu Id-li category artiq movcuddur}");
                return View();
            }
            Product product = new()
            {
                Name = productVM.Name,
                Price = productVM.Price,
                SKU = productVM.SKU,
                CategoryId = (int)productVM.CategoryId,
                Description = productVM.Description,
            };
            
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
