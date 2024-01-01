using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaAb104.Areas.ProniaAdmin.ViewModels;
using ProniaAb104.DAL;
using ProniaAb104.Models;

namespace ProniaAba104.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class SizeController : Controller
    {
        private readonly AppDbContext _db;
        public SizeController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Size> sizes = await _db.Sizes.Include(s => s.ProductSizes).ToListAsync();

            return View(sizes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateSizeVM sizeVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool result = _db.Sizes.Any(t => t.Name == sizeVM.Name);

            if (result)
            {
                ModelState.AddModelError("Name", "Bu Tag artiq movcuddur");
                return View();
            }

            Size size = new()
            {
                Name = sizeVM.Name
            };

            await _db.Sizes.AddAsync(size);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();

            Size size = await _db.Sizes.FirstOrDefaultAsync(s => s.Id == id);

            if (size is null) return NotFound();

            UpdateSizeVM sizeVM = new()
            {
                Name = size.Name
            };

            return View(sizeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateSizeVM sizeVM)
        {
            if (!ModelState.IsValid) return View();

            Size existed = await _db.Sizes.Include(s => s.ProductSizes).FirstOrDefaultAsync(s => s.Id == id);
            if (existed is null) return NotFound();

            bool result = _db.Sizes.Any(s => s.Name == sizeVM.Name);

            if (result)
            {
                ModelState.AddModelError("Name", "Bu Size Artiq movcuddur");
                return View();
            }

            existed.Name = sizeVM.Name;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            Size existed = await _db.Sizes.FirstOrDefaultAsync(s => s.Id == id);
            if (existed is null) return NotFound();

            _db.Sizes.Remove(existed);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Detail(int id)
        {
            if (id <= 0) return BadRequest();

            Size size = _db.Sizes.FirstOrDefault(s => s.Id == id);

            if (size is null) return NotFound();

            return View(size);
        }
    }
}
