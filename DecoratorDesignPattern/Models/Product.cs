﻿using Microsoft.EntityFrameworkCore;

namespace DecoratorDesignPattern.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string UserId { get; set; }
    }
}
