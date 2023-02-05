namespace ShopBridgeAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? DiscountPercentage { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
