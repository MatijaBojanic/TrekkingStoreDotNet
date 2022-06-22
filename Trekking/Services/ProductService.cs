using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Trekking.Models.DB;


namespace Trekking.Services
{
    public class ProductService
    {
        public static List<ProductModel> GetProducts()
        {
            try
            {
                string connectionString = System.Configuration
                    .ConfigurationManager
                    .ConnectionStrings["TrekkingDB"]
                    .ConnectionString;
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand();
                selectCommand.Connection = connection;
                selectCommand.CommandText = "SELECT * FROM products";
                SqlDataReader reader = selectCommand.ExecuteReader();
                List<ProductModel> result = new List<ProductModel>();

                while (reader.Read())
                {
                    ProductModel product = new ProductModel();
                    product.ProductID = reader.GetInt32(0);
                    product.Price = reader.GetDecimal(1);
                    product.Name = reader.GetString(2);
                    result.Add(product);
                }

                connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public static ProductModel GetProductById(int productId)
        {
            try
            {
                string connectionString = System.Configuration
                    .ConfigurationManager
                    .ConnectionStrings["TrekkingDB"]
                    .ConnectionString;
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                
                SqlCommand selectCommand = new SqlCommand();
                selectCommand.Connection = connection;
                selectCommand.CommandText = "SELECT * FROM products WHERE id = @ProductId";
                selectCommand.Parameters.AddWithValue("ProductId", productId);
                SqlDataReader reader = selectCommand.ExecuteReader();
                
                ProductModel product = new ProductModel();
                reader.Read();
                
                product.ProductID = reader.GetInt32(0);
                product.Price = reader.GetDecimal(1);
                product.Name = reader.GetString(2);
                

                connection.Close();
                return product;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        
        public static bool? CreateProduct(ProductModel product)
        {
            try
            {
                string connectionString = System.Configuration
                    .ConfigurationManager
                    .ConnectionStrings["TrekkingDB"]
                    .ConnectionString;
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();


                SqlCommand insertCommand = new SqlCommand();
                insertCommand.Connection = connection;
                insertCommand.CommandText = "INSERT INTO products ( name, price) " +
                                            "VALUES ( @name, @price)";

                // insertCommand.Parameters.AddWithValue("id", product.ProductID);
                insertCommand.Parameters.AddWithValue("name", product.Name);
                insertCommand.Parameters.AddWithValue("price", product.Price);

                int rowsAffected = insertCommand.ExecuteNonQuery();

                connection.Close();

                return rowsAffected == 1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool UpdateProduct(ProductModel product)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                try
                {
                    string connectionString = System.Configuration
                        .ConfigurationManager
                        .ConnectionStrings["TrekkingDB"]
                        .ConnectionString;
                    connection.ConnectionString = connectionString;
                    connection.Open();

                    SqlCommand updateCommand = new SqlCommand();
                    updateCommand.Connection = connection;
                    updateCommand.CommandText = "Update products " +
                                                "SET name = @Name, " +
                                                "price = @Price " +
                                                "WHERE ProductId = @ProductID";

                    updateCommand.Parameters.AddWithValue("ProductID", product.ProductID);
                    updateCommand.Parameters.AddWithValue("Name", product.Name);
                    updateCommand.Parameters.AddWithValue("Price", product.Price);


                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    connection.Close();

                    return rowsAffected == 1;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public static bool DeleteProduct(int productId)
        {
            using(SqlConnection connection = new SqlConnection())
            {
                try
                {
                    string connectionString = System.Configuration
                        .ConfigurationManager
                        .ConnectionStrings["TrekkingDB"]
                        .ConnectionString;
                    connection.ConnectionString = connectionString; 
                    connection.Open();

                    SqlCommand deleteCommand = new SqlCommand();
                    deleteCommand.Connection = connection;
                    deleteCommand.CommandText = "DELETE FROM products WHERE " +
                                                "id = @ProductID";
                    deleteCommand.Parameters.AddWithValue("id", productId);

                    int rowsAffected = deleteCommand.ExecuteNonQuery(); 

                    connection.Close();
                    return rowsAffected == 1; 
                }
                catch(Exception ex)
                {
                    return false; 
                }
            }
        }
        
    }
}