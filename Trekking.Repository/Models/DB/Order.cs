using System;

namespace Trekking.Repository.Models.DB
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public DateTime? PlacedAt { get; set; }
    }
}