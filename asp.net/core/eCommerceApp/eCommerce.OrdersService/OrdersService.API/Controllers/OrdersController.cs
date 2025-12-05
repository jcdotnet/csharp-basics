using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OrdersService.BusinessLogicLayer.DTO;
using OrdersService.BusinessLogicLayer.ServiceContracts;
using OrdersService.DataAccessLayer.Entities;

namespace OrdersService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _service;

        public OrdersController(IOrdersService service)
        {
            _service = service;
        }

        // GET api/orders
        [HttpGet]
        public async Task<IEnumerable<OrderResponse?>> Get()
        {
            return await _service.GetOrders();
        }

        // GET api/orders/search/order/{orderId}
        [HttpGet("search/order/{orderId}")]
        public async Task<OrderResponse?> GetOrder(Guid orderId)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.OrderId, orderId);
            return await _service.GetOrder(filter);
        }

        // GET api/orders/search/product/{productId}
        [HttpGet("search/product/{productId}")]
        public async Task<IEnumerable<OrderResponse?>> GetByProductId(Guid productId)
        {
            var filter = Builders<Order>.Filter.ElemMatch(o => o.OrderItems,
                Builders<OrderItem>.Filter.Eq(p => p.ProductId, productId));
            return await _service.GetOrders(filter);
        }

        // GET api/orders/search/date/{orderDate}
        [HttpGet("search/date/{orderDate}")]
        public async Task<IEnumerable<OrderResponse?>> GetByDate(DateTime orderDate)
        {
            var filter = Builders<Order>.Filter.Eq(o => 
                o.OrderDate.ToString("yyyy-MM-dd"), orderDate.ToString("yyyy-MM-dd"));
            return await _service.GetOrders(filter);
        }

        // GET api/orders/search/user/{userId}
        [HttpGet("search/user/{userId}")]
        public async Task<IEnumerable<OrderResponse?>> GetByUser(Guid userId)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.UserId, userId);
            return await _service.GetOrders(filter);
        }

        // POST api/orders
        [HttpPost]
        public async Task<IActionResult> Post(OrderAddRequest? orderAddRequest)
        {
            if (orderAddRequest is null) return BadRequest("Invalid order");
            var order = await _service.AddOrder(orderAddRequest);
            if (order is null) return Problem("An error has occurred!");
            return Created($"api/orders/search/orderid/{order.OrderId}", order);
        }

        // PUT api/orders/{orderId}
        [HttpPut("{orderId}")]
        public async Task<IActionResult> Put(Guid orderId, OrderUpdateRequest? orderUpdateRequest)
        {
            if (orderUpdateRequest is null || orderId != orderUpdateRequest.OrderId) 
                return BadRequest("Invalid order");
            var order = await _service.UpdateOrder(orderUpdateRequest);
            if (order is null) return Problem("An error has occurred!");
            return Ok(order);
        }

        // DELETE api/orders/{orderId}
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> Put(Guid orderId)
        {
            if (orderId == Guid.Empty) return BadRequest("Invalid order");
            var isDeleted = await _service.DeleteOrder(orderId);
            if (!isDeleted) return Problem("An error has occurred!");
            return Ok(isDeleted);
        }
    }
}
