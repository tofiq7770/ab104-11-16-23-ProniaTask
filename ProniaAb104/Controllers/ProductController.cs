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

        public IActionResult Detail(int id)
        {
            if (id <= 0) 
            {
                return BadRequest();
            }

            Product product = _context.Products.
            Include(p => p.Images).
            Include(p => p.Category).
            Include(p=>p.ProductColors).
            ThenInclude(pc=>pc.Color).
            Include(p=>p.ProductSizes).
            ThenInclude(ps=>ps.Size).
            Include(p => p.ProductTags).
            ThenInclude(pt=>pt.Tag).
            FirstOrDefault(p => p.Id == id);
            
            if(product == null)
            {
                return NotFound();
            }

            List<Product> SimilarProducts = _context.Products.Include(product => product.Images.Where(pi=>pi.IsPrimary!=null)).Where(p => p.CategoryId == product.CategoryId && product.Id != p.Id).Take(3).ToList();

            ProductVM productVMcs = new ProductVM
            {
                Product= product,
                SimilarProducts= SimilarProducts
            };


            return View(productVMcs);
        }


    }
}
