using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace LiteShop.Entities
{
    [Table("Produkty")]
    public class Product
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nazwa produktu")]
        public string? Name { get; set; }

        [Display(Name = "Klient")]
        public virtual ICollection<Customer>? Customers { get; set; }

        [Display(Name = "Oceny")]
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
