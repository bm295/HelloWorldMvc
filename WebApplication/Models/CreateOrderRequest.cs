using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MilkCoPOS.Models
{
    public class CreateOrderRequest
    {
        [Required]
        [StringLength(100)]
        public string Customer { get; set; }

        [Required]
        [MinLength(1)]
        public List<CreateOrderItemRequest> Items { get; set; } = new List<CreateOrderItemRequest>();
    }

    public class CreateOrderItemRequest
    {
        [Required]
        public int InventoryItemId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
