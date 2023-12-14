
using ProniaAb104.Models;
using System.ComponentModel.DataAnnotations;

namespace ProniaAb104.Areas.ProniaAdmin.ViewModels
{
    public class UpdateProductVM
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
