using System.Collections.Generic;
using System.Web.Http;
using Trekking.Repository.DBOperations;
using Trekking.Repository.Models.DB;
using Trekking.Services;


namespace Trekking.Controllers.api
{
    public class OrderController : ApiController
    {
        
        [Route("api/user/{userId}/order")]
        [HttpGet]
        public Order Index(int userId)
        {
            return OrderService.GetActiveUserOrder(userId);
        }

        [Route("api/order/{orderId}/order-items")]
        [HttpGet]
        public List<OrderItem> GetOrderItemsForOrderId(int orderId)
        {
            return OrderService.GetOrderItemsForOrderId(orderId);
        }
        
        [Route("api/order-items")]
        [HttpPost]
        public bool? CreateOrderItem(OrderItem orderItem)
        {
            return OrderService.CreateOrderItem(orderItem);
        }
        
        [Route("api/order-items")]
        [HttpPatch]
        public bool? UpdateOrderItem(OrderItem orderItem)
        {
            return OrderService.UpdateOrderItem(orderItem);
        }
        
        [Route("api/order-items/{orderItemId}")]
        [HttpDelete]
        public bool? DeleteOrderItem(int orderItemId)
        {
            return OrderService.DeleteOrderItem(orderItemId);
        }
    }
}