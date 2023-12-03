﻿using System.ComponentModel.DataAnnotations;

namespace ProniaAb104.Models
{
    public class Slide
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string SubTitle { get; set; }

        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        public int Order { get; set; }

    }
}
