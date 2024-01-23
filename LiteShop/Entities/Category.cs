using System.ComponentModel.DataAnnotations;

namespace LiteShop.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Kategoria")]
        public string Name { get; set; }    

        [Display(Name = "Produkty")]
        public virtual ICollection<Product>? Products { get; set; }
    }
}
