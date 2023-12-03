using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaAb104.DAL;
using ProniaAb104.Models;
using ProniaAb104.ViewModels;

namespace ProniaAb104.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {


            List<Slide> slides = await _context.Slides.OrderBy(s => s.Order).Take(5).ToListAsync();

            List<Product> products = await _context.Products.Take(8).OrderBy(s => s.Id).Include(p => p.ProductImages.Where(pi => pi.IsPrimary != null)).ToListAsync();
     
            //_context.Slides.AddRange(slides);
            //_context.SaveChanges();

            HomeVM home = new HomeVM
            {
                Slides = slides,
                Products = products
            };


            return View(home);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
