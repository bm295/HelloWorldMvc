using System.ComponentModel.DataAnnotations;

namespace MilkCoPOS.Models
{
    public class InventoryItem
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        [StringLength(120)]
        public string Name { get; set; }

        public int Quantity { get; set; }
    }
}
