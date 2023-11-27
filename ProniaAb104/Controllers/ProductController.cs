using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaAb104.DAL;
using ProniaAb104.Models;

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
        public IActionResult Detail(int id)
        {
            if (id <= 0)return BadRequest();

            Product product = _context.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
            if (product is null) return NotFound();
            
            return View(product);
        }
    }
}
