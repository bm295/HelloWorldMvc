using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MilkCoPOS.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        [StringLength(100)]
        public string Customer { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
