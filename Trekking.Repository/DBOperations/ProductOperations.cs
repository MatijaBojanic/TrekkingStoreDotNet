using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Trekking.Repository.Models.DB;

namespace Trekking.Repository.DBOperations
{
    public class ProductOperations
    {
        public static List<ProductModel> GetProducts(string connectionString)
        {
            try
            {
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
                    product.ProductId = reader.GetInt32(0);
                    product.Name = reader.GetString(1);
                    product.Price = reader.GetDecimal(2);
                    product.Path = SafeGetString(reader,3);
                    product.Description = SafeGetString(reader,4);
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
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                
                SqlCommand selectCommand = new SqlCommand();
                selectCommand.Connection = connection;
                selectCommand.CommandText = "SELECT * FROM products WHERE id = @ProductId";
                selectCommand.Parameters.AddWithValue("ProductId", productId);
                SqlDataReader reader = selectCommand.ExecuteReader();
                
                ProductModel product = new ProductModel();
                reader.Read();
                
                product.ProductId = reader.GetInt32(0);
                product.Name = reader.GetString(1);
                product.Price = reader.GetDecimal(2);
                product.Path = reader.GetString(3);
                product.Description = reader.GetString(4);


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
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();


                SqlCommand insertCommand = new SqlCommand();
                insertCommand.Connection = connection;
                insertCommand.CommandText = "INSERT INTO products ( name, price, path, description) " +
                                            "VALUES ( @name, @price, @path, @description)";

                insertCommand.Parameters.AddWithValue("name", product.Name);
                insertCommand.Parameters.AddWithValue("price", product.Price);
                insertCommand.Parameters.AddWithValue("description", product.Description ?? "" );
                insertCommand.Parameters.AddWithValue("path", product.Path ?? "");

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
                    connection.ConnectionString = connectionString;
                    connection.Open();

                    SqlCommand updateCommand = new SqlCommand();
                    updateCommand.Connection = connection;
                    updateCommand.CommandText = "Update products " +
                                                "SET name = @Name, " +
                                                "price = @Price " +
                                                "WHERE ProductId = @ProductId";

                    updateCommand.Parameters.AddWithValue("ProductId", product.ProductId);
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
                    connection.ConnectionString = connectionString; 
                    connection.Open();

                    SqlCommand deleteCommand = new SqlCommand();
                    deleteCommand.Connection = connection;
                    deleteCommand.CommandText = "DELETE FROM products WHERE " +
                                                "id = @ProductId";
                    deleteCommand.Parameters.AddWithValue("ProductId", productId);

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
        
        public static string SafeGetString(SqlDataReader reader, int colIndex)
        {
            if(!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }
    }
}