using System.Collections.Generic;
using Trekking.Repository.DBOperations;
using Trekking.Repository.Models.DB;

namespace Trekking.Services
{
    public class ProductService
    {
        public static List<ProductModel> GetProducts()
        {
            string connectionString = System.Configuration.
                                            ConfigurationManager.
                                            ConnectionStrings["TrekkingDB"].
                                            ConnectionString;
            return ProductOperations.GetProducts(connectionString);
        }
        
        public static ProductModel GetProductById(int productId)
        {
            string connectionString = System.Configuration.
                ConfigurationManager.
                ConnectionStrings["TrekkingDB"].
                ConnectionString;
            return ProductOperations.GetProductById(productId, connectionString);
        }

        
        public static bool? CreateProduct(ProductModel product)
        {
            string connectionString = System.Configuration.
                ConfigurationManager.
                ConnectionStrings["TrekkingDB"].
                ConnectionString;
            return ProductOperations.CreateProduct(product, connectionString);
        }

        public static bool UpdateProduct(ProductModel product)
        {
            string connectionString = System.Configuration.
                ConfigurationManager.
                ConnectionStrings["TrekkingDB"].
                ConnectionString;
            return ProductOperations.UpdateProduct(product, connectionString);
        }
        
        public static bool DeleteProduct(int productId)
        {
            string connectionString = System.Configuration.
                ConfigurationManager.
                ConnectionStrings["TrekkingDB"].
                ConnectionString;
            return ProductOperations.DeleteProduct(productId, connectionString);
        }
    }
}