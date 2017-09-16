using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Domain
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [StringLength(maximumLength:50,MinimumLength =3)]
        [Index("Product_Description_Index")]  
        public string Description { get; set; }

        [Required]
        [DisplayFormat(DataFormatString ="{0:C2}",ApplyFormatInEditMode =false)]
        public decimal  Preci { get; set; }

        [Display(Name ="Is Active?")]
        public bool  IsActive { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }
        public double  Stock { get; set; }

        [Display(Name ="Last purchase")]
        [DataType(DataType.Date)]
        public DateTime LastPurchase { get; set; }
        
        [JsonIgnore]
        public virtual Category Category { get; set; }
    }
}
