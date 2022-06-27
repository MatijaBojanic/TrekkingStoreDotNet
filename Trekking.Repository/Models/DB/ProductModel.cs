namespace Trekking.Repository.Models.DB
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
        
        public string Description { get; set; }
        
        public string Path { get; set;  }
        
        public byte[] ProductImage { get; set; }
    }
}