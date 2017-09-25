using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Models
{
    public class Product
    {
        [JsonProperty("LastPurchase")]
        public DateTime LastPurchase { get; set; }

        [JsonProperty("Image")]
        public string Image { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("IsActive")]
        public bool IsActive { get; set; }

        [JsonProperty("ProductId")]
        public int ProductId { get; set; }

        [JsonProperty("Preci")]
        public decimal Preci { get; set; }

        [JsonProperty("Remarks")]
        public string Remarks { get; set; }

        [JsonProperty("Stock")]
        public double Stock { get; set; }

        public string ImageFullPath { get { return string.Format("http://productsbackend82.azurewebsites.net/{0}", Image.Substring(1)); } }
    }
}
