using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Trekking.Repository.Models.DB;

namespace Trekking.Repository.DBOperations
{
    public class ProductOperations
    {
        public static List<ProductModel> GetProducts(string connectionString)
        {
            try
            {
                // string connectionString = System.Configuration
                //     .ConfigurationManager
                //     .ConnectionStrings["TrekkingDB"]
                //     .ConnectionString;
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
                    product.ProductId = reader.GetInt32(2);
                    product.Price = reader.GetDecimal(0);
                    product.Name = reader.GetString(1);
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
        
        public static ProductModel GetProductById(int productId, string connectionString)
        {
            try
            {
                // string connectionString = System.Configuration
                //     .ConfigurationManager
                //     .ConnectionStrings["TrekkingDB"]
                //     .ConnectionString;
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                
                SqlCommand selectCommand = new SqlCommand();
                selectCommand.Connection = connection;
                selectCommand.CommandText = "SELECT * FROM products WHERE ID = @ProductId";
                selectCommand.Parameters.AddWithValue("ProductId", productId);
                SqlDataReader reader = selectCommand.ExecuteReader();
                
                ProductModel product = new ProductModel();
                reader.Read();
                
                product.Price = reader.GetDecimal(0);
                product.Name = reader.GetString(1);
                product.ProductId = reader.GetInt32(2);


                connection.Close();
                return product;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        
        public static bool? CreateProduct(ProductModel product, string connectionString)
        {
            try
            {
                // string connectionString = System.Configuration
                //     .ConfigurationManager
                //     .ConnectionStrings["TrekkingDB"]
                //     .ConnectionString;
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

        public static bool UpdateProduct(ProductModel product, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                try
                {
                    // string connectionString = System.Configuration
                    //     .ConfigurationManager
                    //     .ConnectionStrings["TrekkingDB"]
                    //     .ConnectionString;
                    connection.ConnectionString = connectionString;
                    connection.Open();

                    SqlCommand updateCommand = new SqlCommand();
                    updateCommand.Connection = connection;
                    updateCommand.CommandText = "Update products " +
                                                "SET name = @Name, " +
                                                "price = @Price " +
                                                "WHERE ProductId = @ProductID";

                    updateCommand.Parameters.AddWithValue("ProductID", product.ProductId);
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
        public static bool DeleteProduct(int productId, string connectionString)
        {
            using(SqlConnection connection = new SqlConnection())
            {
                try
                {
                    // string connectionString = System.Configuration
                    //     .ConfigurationManager
                    //     .ConnectionStrings["TrekkingDB"]
                    //     .ConnectionString;
                    connection.ConnectionString = connectionString; 
                    connection.Open();

                    SqlCommand deleteCommand = new SqlCommand();
                    deleteCommand.Connection = connection;
                    deleteCommand.CommandText = "DELETE FROM products WHERE " +
                                                "id = @ProductID";
                    deleteCommand.Parameters.AddWithValue("ProductID", productId);

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