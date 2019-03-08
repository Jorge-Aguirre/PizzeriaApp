namespace BusinessDomain.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public int ProductTypeId { get; set; }

        public string Name { get; set; }

        public ProductType ProductType { get; set; }

        public int CookTime { get; set; }
    }
}
