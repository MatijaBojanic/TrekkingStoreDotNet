using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Trekking.Repository.Models.DB;

namespace Trekking.Repository.DBOperations
{
    public class OrderOperations
    {
        public static Order GetActiveUserOrder(string connectionString,int userId)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                
                SqlCommand selectCommand = new SqlCommand();
                selectCommand.Connection = connection;
                selectCommand.CommandText = "SELECT * FROM orders where user_id=@userId and placed_at IS NULL";
                selectCommand.Parameters.AddWithValue("userId", userId);
                SqlDataReader reader = selectCommand.ExecuteReader();
                
                Order order = new Order();
                
                if (!reader.Read())
                {
                    reader.Close();
                    
                    SqlCommand insertCommand = new SqlCommand();
                    insertCommand.Connection = connection;
                    insertCommand.CommandText = "INSERT INTO orders(user_id) values(@user_id)";
                    insertCommand.Parameters.AddWithValue("user_id", userId);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    
                    SqlDataReader readerNew = selectCommand.ExecuteReader();
                    readerNew.Read();
                    order.OrderId = readerNew.GetInt32(0);
                    order.UserId = readerNew.GetInt32(1);
                    order.Price = readerNew.GetDecimal(2);
                    if(!readerNew.IsDBNull(3))
                        order.PlacedAt = readerNew.GetDateTime(3);
                }
                else
                {
                    order.OrderId = reader.GetInt32(0);
                    order.UserId = reader.GetInt32(1);
                    order.Price = reader.GetDecimal(2);
                    
                    if(!reader.IsDBNull(3))
                        order.PlacedAt = reader.GetDateTime(3);    
                }
                
                connection.Close();
                return order;
            }
            catch (Exception ex)
            {
                return null;
            }   
        }

        public static List<OrderItem> GetOrderItemsForOrderId(string connectionString, int  orderId)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand();
                selectCommand.Connection = connection;
                selectCommand.CommandText = "SELECT * FROM order_items where order_id=@orderId";
                selectCommand.Parameters.AddWithValue("orderId", orderId);
                SqlDataReader reader = selectCommand.ExecuteReader();
                List<OrderItem> result = new List<OrderItem>();

                while (reader.Read())
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.OrderItemId = reader.GetInt32(0);
                    orderItem.OrderId = reader.GetInt32(1);
                    orderItem.ProductId = reader.GetInt32(2);
                    orderItem.Quantity = reader.GetInt32(3);
                    result.Add(orderItem);
                }

                connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }   
        }

        public static bool? CreateOrderItem(string connectionString, OrderItem orderItem)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand();
                selectCommand.Connection = connection;
                selectCommand.CommandText = "SELECT * FROM order_items where order_id=@orderId and product_id=@productId";
                selectCommand.Parameters.AddWithValue("orderId", orderItem.OrderId);
                selectCommand.Parameters.AddWithValue("productId", orderItem.ProductId);

                SqlDataReader reader = selectCommand.ExecuteReader();
                SqlCommand insertCommand = new SqlCommand();
                if (!reader.Read())
                {
                    insertCommand.Connection = connection;
                    insertCommand.CommandText = "INSERT INTO order_items(order_id, product_id, quantity) values(@orderId, @productId, @quantity)";
                    insertCommand.Parameters.AddWithValue("orderId", orderItem.OrderId);
                    insertCommand.Parameters.AddWithValue("productId", orderItem.ProductId);
                    insertCommand.Parameters.AddWithValue("quantity", 1);
                }
                else
                {
                    insertCommand.Connection = connection;
                    insertCommand.CommandText = "UPDATE order_items set quantity=@quantity where id=@orderItemId";
                    insertCommand.Parameters.AddWithValue("quantity", reader.GetInt32(3) + 1);
                    insertCommand.Parameters.AddWithValue("orderItemId", reader.GetInt32(0));
                }
                reader.Close();
                int rowsAffected = insertCommand.ExecuteNonQuery();
                connection.Close();
                return rowsAffected == 1;
            }
            catch (Exception ex)
            {
                return null;
            }   
        }

        public static bool? UpdateOrderItem(string connectionString, OrderItem orderItem)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand insertCommand = new SqlCommand();
                insertCommand.Connection = connection;
                insertCommand.CommandText = "UPDATE order_items set  quantity = @quantity where id=@orderItemId";
                insertCommand.Parameters.AddWithValue("quantity", orderItem.Quantity);
                insertCommand.Parameters.AddWithValue("orderItemId", orderItem.OrderItemId);
                int rowsAffected = insertCommand.ExecuteNonQuery();
                connection.Close();
                return rowsAffected == 1;
            }
            catch (Exception ex)
            {
                return null;
            }   
        }
        
        
        public static bool? DeleteOrderItem(string connectionString, int orderItemId)
        {
            using(SqlConnection connection = new SqlConnection())
            {
                try
                {
                    connection.ConnectionString = connectionString; 
                    connection.Open();

                    SqlCommand deleteCommand = new SqlCommand();
                    deleteCommand.Connection = connection;
                    deleteCommand.CommandText = "DELETE FROM order_items WHERE " +
                                                "id = @orderItemId";
                    deleteCommand.Parameters.AddWithValue("OrderItemId", orderItemId);

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