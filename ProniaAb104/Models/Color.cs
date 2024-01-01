using System.ComponentModel.DataAnnotations;

namespace ProniaAb104.Models
{
    public class Color
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ad mutleqdir")]
        [MaxLength(25, ErrorMessage = "Uzunlug 25-i kece bilmez")]
        public string Name { get; set; }
        public List<ProductColor>? ProductColors { get; set; }
        
    }
}
