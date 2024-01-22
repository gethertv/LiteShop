using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace LiteShop.Entities
{
    [Table("Zamowienia")]
    public class Order
    {

  


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Data zakupu")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataS { get; set; }

        [ForeignKey("CustomerId")]
        public int? CustomerId { get; set; }

        [Display(Name = "Klient")]
        public Customer? Customer { get; set; }

        [Display(Name = "Szczegoly zamowienia")]
        public ICollection<OrderDetail>? OrderDetails { get; set; }

        public string IN
        {
            get
            {
                return " ";
            }
        }
        
    }
}
