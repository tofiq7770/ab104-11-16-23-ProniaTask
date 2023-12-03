using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Create(Slide slide)
        {


            if (slide.Photo is null)
            {
                ModelState.AddModelError("Photo", "Mutlew Shekil secilmelidir");
                return View();
            }
            if (!slide.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "File tipi uygun deyil");
                return View();
            }
            if (!slide.Photo.ValidateSize(3 * 1024))
            {
                ModelState.AddModelError("Photo", "Fotonun hecmi 3mb den boyuk OLMAMALIDIR");
                return View();
            }



            slide.Image = await slide.Photo.CreateFile(_env.WebRootPath, "assets", "images", "slider");

            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

            //return Content(slide.Photo.FileName + " " + slide.Photo.ContentType + " " + slide.Photo.Length);

        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Slide slide = await _context.Slides.FirstOrDefaultAsync(c => c.Id == id);
            if (slide == null) return NotFound();

            return View(slide);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,Slide slide)
        {
            Slide existed = await _context.Slides.FirstOrDefaultAsync(c => c.Id == id);

            if (existed == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(existed);
            }


            if (slide.Photo is not null)
            {

                if (!slide.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File tipi uygun deyil");
                    return View(existed);
                }
                if (!slide.Photo.ValidateSize(3 * 1024))
                {
                    ModelState.AddModelError("Photo", "Fotonun hecmi 3mb den boyuk OLMAMALIDIR");
                    return View(existed);
                }
                string newImage =await slide.Photo.CreateFile(_env.WebRootPath, "assets", "images", "slider");
                existed.Image.DeleteFile(_env.WebRootPath, "assets", "images", "slider");
                existed.Image = newImage;
            }


            existed.Title = slide.Title;
            existed.Description = slide.Description;
            existed.SubTitle = slide.SubTitle;
            existed.Order = slide.Order;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Slide existed = await _context.Slides.FirstOrDefaultAsync(c => c.Id == id);
            if (existed == null) return NotFound();
            existed.Image.DeleteFile(_env.WebRootPath,"assets","images","slider");
            _context.Slides.Remove(existed);
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
