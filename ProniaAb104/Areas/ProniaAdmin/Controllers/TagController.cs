﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaAb104.Areas.ProniaAdmin.ViewModels;
using ProniaAb104.DAL;
using ProniaAb104.Models;
using System.Drawing;

namespace ProniaAb104.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class TagController : Controller
    {
        private readonly AppDbContext _db;
        public TagController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Tag> tags = await _db.Tags.Include(t => t.ProductTags).ToListAsync();

            return View(tags);
        }

        //Get//
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTagVM tagVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            bool result = _db.Tags.Any(t => t.Name == tagVM.Name);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu Adda tag artiq movcuddur");
                return View();
            }

            Tag tag = new()
            {
                Name = tagVM.Name
            };

            await _db.Tags.AddAsync(tag);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");


        }

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) BadRequest();

            Tag tag = await _db.Tags.FirstOrDefaultAsync(t => t.Id == id);

            if (tag is null) return NotFound();

            UpdateTagVM tagVM = new()
            {
                Name = tag.Name
            };

            return View(tagVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateTagVM tagVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Tag existed = await _db.Tags.FirstOrDefaultAsync(t => t.Id == id);
            if (existed is null) return NotFound();

            bool result = _db.Tags.Any(t => t.Name.ToLower().Trim() == tagVM.Name.ToLower().Trim());

            if (result)
            {
                ModelState.AddModelError("Name", "Bu Adda Tag movcuddur");
                return View(tagVM);
            }

            existed.Name = tagVM.Name;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            if (Id <= 0) return BadRequest();

            Tag existed = await _db.Tags.FirstOrDefaultAsync(t => t.Id == Id);

            if (existed is null) return NotFound();

            _db.Tags.Remove(existed);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int id)
        {
            if (id <= 0) return BadRequest();

            Tag tag = _db.Tags.FirstOrDefault(t => t.Id == id);

            if (tag is null) return NotFound();

            return View(tag);

        }
    }
}
