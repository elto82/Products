using Products.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Products.Backend.Models
{
    [NotMapped]
    public class ProductView: Product
    {
        [Display(Name ="Image")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}