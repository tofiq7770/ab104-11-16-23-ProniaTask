using ProniaAb104.Models;
using System.ComponentModel.DataAnnotations;

namespace ProniaAb104.Areas.ProniaAdmin.ViewModels
{
    public class UpdateSizeVM
    {
        
        [Required(ErrorMessage = "Ad mutleq daxil edilmelidir")]
        [MaxLength(25, ErrorMessage = "Uzunlug max 25 olmalidir")]
        public string Name { get; set; }
        
    }
}
