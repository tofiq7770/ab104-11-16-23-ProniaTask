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
        public IActionResult Index()
        {
            #region SQL
            //List<Slide> slides = new List<Slide>
            //{
            //    new Slide
            //    {

            //        Title ="Title 1",
            //        SubTitle="SubTitle 1",
            //        Description ="Very Good 1",
            //        Image="slide 1.jfif",
            //        Order = 3,
            //    },

            //    new Slide
            //    {

            //        Title ="Title 2",
            //        SubTitle="SubTitle 2",
            //        Description ="Very Good 2",
            //        Image="slide 2.jfif",
            //        Order = 1,
            //    },

            //    new Slide
            //    {

            //        Title ="Title 3",
            //        SubTitle="SubTitle 3",
            //        Description ="Very Good 3",
            //        Image="slide 3.jfif",
            //        Order = 4,
            //    },

            //    new Slide
            //    {

            //        Title ="Title 4",
            //        SubTitle="SubTitle 4",
            //        Description ="Very Good 4",
            //        Image="slide 4.jfif",
            //        Order = 2,
            //    }
            //};

            //_context.Slides.AddRange(slides);
            //_context.SaveChanges();
            #endregion

            List<Slide> slides = _context.Slides.OrderBy(s=>s.Order).Take(3).ToList();
            List<Product> products = _context.Products.Include(p => p.ProductImages.Where(pi => pi.IsPrimary != null)).OrderBy(s => s.Id).ToList();

            //return View(slides.Take(4).OrderBy(s=>s.Order).ToList()); sqlden datani goturub orderby edirik

            HomeVM home = new HomeVM()
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
