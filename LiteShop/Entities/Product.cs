using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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

        [Required]
        [Display(Name = "Cena")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double? Price { get; set; }


        [Display(Name = "Klient")]
        public virtual ICollection<Customer>? Customers { get; set; }

        [Display(Name = "Szczegóły zamówienia")]
        public ICollection<OrderDetail>? OrderDetails { get; set; }

        [Required(ErrorMessage = "Wybór kategorii jest obowiązkowy.")]
        [Display(Name = "Kategoria")]
        public int? CategoryId { get; set; }

        [Display(Name = "Kategoria")]
        public Category? Category { get; set; }

    }
}
