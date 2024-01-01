﻿using ProniaAb104.Models;

namespace ProniaAb104.Models
{
    public class ProductSize
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }

        public Product Product { get; set; }
        public Size Size { get; set; }
    }
}