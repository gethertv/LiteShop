using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiteShop.Entities
{
    /**
     * Tabela posredniczaca miedzy Order a Product 
     */
    [Table("Sczegoly_Zamowienia")]
    public class OrderDetail
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailId { get; set; }

        [ForeignKey("OrderId")]
        public int OrderId { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        [Display(Name = "Zamowienie")]
        public Order? Order { get; set; }

        [Display(Name = "Przedmiot")]
        public Product? Product{ get; set; }

        [Display(Name = "Ilość")]
        public int Amount { get; set; } = 1;


    }
}
