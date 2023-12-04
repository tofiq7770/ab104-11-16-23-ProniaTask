using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaAb104.Areas.ProniaAdmin.ViewModels;
using ProniaAb104.DAL;
using ProniaAb104.Models;
using ProniaAb104.Utilities.Extensions;

namespace ProniaAb104.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]

    public class SlideController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SlideController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slide> slides = await _context.Slides.ToListAsync();

            return View(slides);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSlideVM slideVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!slideVm.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "File tipi uygun deyil");
                return View();
            }
            if (!slideVm.Photo.ValidateSize(3 * 1024))
            {
                ModelState.AddModelError("Photo", "Fotonun hecmi 3mb den boyuk OLMAMALIDIR");
                return View();
            }



            string fileName = await slideVm.Photo.CreateFile(_env.WebRootPath, "assets", "images", "slider");


            Slide slide = new Slide()
            {
                Image = fileName,
                Title = slideVm.Title,
                SubTitle = slideVm.SubTitle,
                Description = slideVm.Description,
                Order = slideVm.Order,
            };
            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

            //return Content(slide.Photo.FileName + " " + slide.Photo.ContentType + " " + slide.Photo.Length);

        }



        #region deletefirst
        //public async Task<IActionResult> Delete(int id)
        //{
        //    if (id <= 0) return BadRequest();

        //    Slide existed = await _context.Slides.FirstOrDefaultAsync(c => c.Id == id);

        //    if (existed is null) return NotFound();

        //    _context.Slides.Remove(existed);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));

        //}
        #endregion
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();

            Slide existed = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (existed is null) return NotFound();

            UpdateSlideVM slideVM = new UpdateSlideVM
            {
                Image = existed.Image,
                Title = existed.Title,
                SubTitle = existed.SubTitle,
                Description = existed.Description,
                Order = existed.Order
            };

            return View(slideVM);

        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateSlideVM slideVM)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            Slide existed = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (existed is null) return NotFound();


            if (slideVM.Photo is not null)
            {
                if (!slideVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File tipi uygun deyil");
                    return View(slideVM);
                }
                if (!slideVM.Photo.ValidateSize(3 * 1024))
                {
                    ModelState.AddModelError("Photo", "Sheklin hecmi 2 mb-den boyuk olmamalidir");
                    return View(slideVM);
                }
                string newImage = await slideVM.Photo.CreateFile(_env.WebRootPath, "assets", "images", "slider");
                existed.Image.DeleteFile(_env.WebRootPath, "assets", "images", "slider");
                existed.Image = newImage;

            }

            existed.Title = slideVM.Title;
            existed.Description = slideVM.Description;
            existed.SubTitle = slideVM.SubTitle;
            existed.Order = slideVM.Order;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));



        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            Slide slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (slide is null) return NotFound();

            slide.Image.DeleteFile(_env.WebRootPath, "assets", "images", "slider");


            _context.Slides.Remove(slide);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Detail(int id)
        {
            if (id <= 0) return BadRequest();
            Slide slide = await _context.Slides.FirstOrDefaultAsync(c => c.Id == id);
            if (slide == null) return NotFound();

            return View(slide);
        }

    }
}
