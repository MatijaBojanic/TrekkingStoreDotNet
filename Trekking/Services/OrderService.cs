using System.Collections.Generic;
using Trekking.Repository.DBOperations;
using Trekking.Repository.Models.DB;

namespace Trekking.Services
{
    public class OrderService
    {
        public static Order GetActiveUserOrder(int userId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TrekkingDB"]
                .ConnectionString;
            return OrderOperations.GetActiveUserOrder(connectionString, userId);
        }

        public static List<OrderItem> GetOrderItemsForOrderId(int orderId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TrekkingDB"]
                .ConnectionString;
            return OrderOperations.GetOrderItemsForOrderId(connectionString, orderId);
        }

        public static bool? CheckoutOrder(Order order)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TrekkingDB"]
                .ConnectionString;
            return OrderOperations.CheckoutOrder(connectionString, order);
        }
        
        public static bool? CreateOrderItem(OrderItem orderItem)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TrekkingDB"]
                .ConnectionString;
            return OrderOperations.CreateOrderItem(connectionString, orderItem);
        }

        public static bool? UpdateOrderItem(OrderItem orderItem)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TrekkingDB"]
                .ConnectionString;
            return OrderOperations.UpdateOrderItem(connectionString, orderItem);
        }
        
        public static bool? DeleteOrderItem(int orderItemId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TrekkingDB"]
                .ConnectionString;
            return OrderOperations.DeleteOrderItem(connectionString, orderItemId);
        }
    }
}