using Microsoft.AspNetCore.Mvc;
using ProniaAb104.Models;

namespace ProniaAb104.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Slide> slides = new List<Slide>
            {
                new Slide
                {
                    Id = 1,
                    Title ="Title 1",
                    SubTitle="SubTitle 1",
                    Description ="Very Good 1",
                    Image="slide 1.jfif",
                    Order = 3,
                },

                new Slide
                {
                    Id = 2,
                    Title ="Title 2",
                    SubTitle="SubTitle 2",
                    Description ="Very Good 2",
                    Image="slide 2.jfif",
                    Order = 1,
                },

                new Slide
                {
                    Id = 3,
                    Title ="Title 3",
                    SubTitle="SubTitle 3",
                    Description ="Very Good 3",
                    Image="slide 3.jfif",
                    Order = 4,
                },
                
                new Slide
                {
                    Id = 4,
                    Title ="Title 4",
                    SubTitle="SubTitle 4",
                    Description ="Very Good 4",
                    Image="slide 4.jfif",
                    Order = 2,
                }
            };
            return View(slides);

        }
        public IActionResult About()
        {
            return View();
        }
    }
}
