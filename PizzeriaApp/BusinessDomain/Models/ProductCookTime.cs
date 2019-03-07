namespace BusinessDomain.Models
{
    public class ProductCookTime
    {
        public int ProductCookTimeId { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Minutes { get; set; }
    }
}
