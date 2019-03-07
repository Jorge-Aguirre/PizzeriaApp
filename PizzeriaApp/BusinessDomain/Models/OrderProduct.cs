namespace BusinessDomain.Models
{
    public class OrderProduct
    {
        public int OrderProductId { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public double UnitPrice { get; set; }

        public int SizeId { get; set; }

        public int Size { get; set; }

        public int Portions { get; set; }

        public State State { get; set; }
    }
}
