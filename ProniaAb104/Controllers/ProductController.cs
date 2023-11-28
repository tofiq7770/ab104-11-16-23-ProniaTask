using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaAb104.DAL;
using ProniaAb104.Models;
using ProniaAb104.ViewModels;

namespace ProniaAb104.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Detail(int id)
        {
            if (id <= 0) return BadRequest();

            Product product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductTags).ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(p => p.Id == id);



            if (product is null) return NotFound();

            List<Product> products = await _context.Products.Include(p => p.ProductImages.Where(pi => pi.IsPrimary != null)).Where(p => p.CategoryId == product.CategoryId && p.Id != product.Id).ToListAsync();


            DetailVM detailVM = new DetailVM
            {
                Product = product,
                RelatedProducts = products,
            };

            return View(detailVM);
        }
    }
}
