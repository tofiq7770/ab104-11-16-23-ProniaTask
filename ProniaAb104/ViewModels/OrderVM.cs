﻿using ProniaAb104.Models;

namespace ProniaAb104.ViewModels
{
    public class OrderVM
    {
        public string Address { get; set; }
        public List<BasketItem>? BasketItems { get; set; }
    }
}
