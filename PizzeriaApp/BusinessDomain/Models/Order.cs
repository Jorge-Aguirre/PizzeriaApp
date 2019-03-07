using System.Collections.Generic;

namespace BusinessDomain.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public string Client { get; set; }

        public OrderState Status { get; set; }

        public ICollection<OrderProduct> Products { get; set; }
    }
}
