namespace ShopBridgeAPI.Models
{
    public class SeedData
    {
        public static List<Product> GetProducts() => new List<Product>
        {
            new Product
            {
                Id = 1,
                Name= "Sapiens - By Yuval Noah Harari",
                Description= "A Brief History of Human Kind",
                CategoryId= 501,
                Price = 460,
            },
            new Product
            {
                Id = 2,
                Name= "Atomic Habits - By James Clear",
                Description= "An Easy and Proven way to create good habits",
                CategoryId= 501,
                Price = 350,
            },
            new Product
            {
                Id = 3,
                Name= "The Psychology of Money - By Morgan Housel",
                Description= "Timeless lessons on wealth, greed and Happiness",
                CategoryId= 501,
                Price = 420,
            },
            new Product
            {
                Id = 4,
                Name= "Thinking fast and slow - By Daniel Kahneman",
                Description= "The two ways we make decisions",
                CategoryId= 501,
                Price = 460,
            },
            new Product
            {
                Id = 5,
                Name= "Wooden Wall Shelf",
                Description= "Decorative set of shelves",
                CategoryId= 502,
                Price = 2000,
            },
            new Product
            {
                Id = 6,
                Name= "Wooden TV Unit",
                Description= "Perfect TV Unit for your home",
                CategoryId= 502,
                Price = 18000,
            },
            new Product
            {
                Id = 7,
                Name= "Homeify Sofa set",
                Description= "Seating Capacity - 8",
                CategoryId= 502,
                Price = 40000,
            },
            new Product
            {
                Id = 8,
                Name= "Shoe rack",
                Description= "Wooden Shoe rack with multiple racks",
                CategoryId= 502,
                Price = 3500,
            },
            new Product
            {
                Id = 9,
                Name= "XYZ Wet Grinder",
                Description= "Best company for your home maker",
                CategoryId= 503,
                Price = 6000,
            },
            new Product
            {
                Id = 10,
                Name= "Godrej Refrigerator",
                Description= "Environment friendly Double door refrigerator",
                CategoryId= 503,
                Price = 18000,
            },
            new Product
            {
                Id = 11,
                Name= "Samsung Mobile charger",
                Description= "Type-C 18W charger",
                CategoryId= 504,
                Price = 1349,
            },
            new Product
            {
                Id = 12,
                Name= "JBL Wireless HeadPhones",
                Description= "Best in class music",
                CategoryId= 504,
                Price = 1499,
            }
        };

        public static List<Category> GetCategories() => new List<Category>
        {
            new Category
            {
                Id = 501,
                Name = "Books",
                DiscountPercentage = 2
            },
            new Category
            {
                Id = 502,
                Name = "Furniture"
            },
            new Category
            {
                Id = 503,
                Name = "Home Appliances"
            },
            new Category
            {
                Id = 504,
                Name = "Mobile Accessories",
                DiscountPercentage = 10
            }
        };
    }
}
