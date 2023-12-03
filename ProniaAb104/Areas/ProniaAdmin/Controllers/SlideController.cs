using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaAb104.DAL;
using ProniaAb104.Models;

namespace ProniaAb104.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]

    public class SlideController : Controller
    {
        private readonly AppDbContext _context;
        public SlideController(AppDbContext context)
        {
            _context = context;
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
            if (!slide.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "File tipi uygun deyil");
                return View();
            }
            if (slide.Photo.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("Photo", "Fotonun hecmi 2mb den boyuk OLMAMALIDIR");
                return View();
            }

            FileStream file = new FileStream(@"C:\Users\tofiq\Desktop\ProniaAb104\ProniaAb104\wwwroot\assets\images\slider\" + slide.Photo.FileName, FileMode.Create);

            await slide.Photo.CopyToAsync(file);
            file.Close();
            slide.Image = slide.Photo.FileName;

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
        //public async Task<IActionResult> Update(Slide slide)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }
        //    //Slide existed = await _context.Slides.FirstOrDefaultAsync(c => c.Id == id);

        //    //if (existed == null) return NotFound();

        //    //bool result = _context.Slides.Any(c => c.Order == slide.Order);

        //    //if (result)
        //    //{
        //    //    ModelState.AddModelError("Name", "Bu Order artiq movcuddur");
        //    //    return View();
        //    //}

        //    //existed.Order = slide.Order;
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //    //return View("Index");
        //}
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            Slide existed = await _context.Slides.FirstOrDefaultAsync(c => c.Id == id);

            if (existed is null) return NotFound();

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
