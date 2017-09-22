﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Products.API.Models
{
    public class ProductResponse
    {
        public int ProductId { get; set; }      
        public string Description { get; set; }     
        public decimal Preci { get; set; }      
        public bool IsActive { get; set; }     
        public string Remarks { get; set; }
        public double Stock { get; set; }
        public DateTime LastPurchase { get; set; }
        public string Image { get; set; }

    }
}