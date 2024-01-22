using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NuGet.Protocol.Plugins;

namespace LiteShop.Entities
{
    public class Customer
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Imie")]
        public string? Name { get; set; }
        [Required]
        [Display(Name = "Nazwisko")]
        public string? Surname { get; set; }

        [Display(Name = "Adres")]
        public string? Address { get; set; }
        
        [Display(Name = "City")]
        public string? City { get; set; }

        [Display(Name = "Lista zamówień")]
        public virtual ICollection<Order>? Students { get; }

        [Display(Name = "Produkty")]
        public virtual ICollection<Product>? Products { get; }


    }
}
