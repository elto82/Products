namespace Products.Domain
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        [Index("CategoryDescriptionIndex", IsUnique = true)]
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
