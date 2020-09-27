using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SelfEducationProjectAPI.Entities;
using SelfEducationProjectAPI.ViewModels;

namespace SelfEducationProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger _logger;
        private OrdersDbContext db;
        public OrdersController(OrdersDbContext context, ILogger<OrdersController> logger)
        {
            db = context;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<OrderVM> Get()
        {
            try
            {
                var orders = from od in db.OrderDetails.ToList()
                             where od.OrderID != null
                             group od by od.OrderID
                             into g
                             select new OrderVM
                             {
                                 OrderNum = g.Key.Value,
                                 TotalPrice = g.Sum(x => x.TotalPrice),
                                 ProductNames = string.Join(", ", g.Select(x => x.Product.ProductName))
                             };

                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new List<OrderVM>();
            }
        }

        [Authorize]
        [HttpPost]
        public bool Post(Order order)
        {
            try
            {
                db.Orders.Add(order);
                db.SaveChanges();
                int orderID = order.Order_ID;

                List<OrderDetail> details = db.OrderDetails.Where(x => x.OrderID == null).ToList();
                foreach (var det in details)
                    det.OrderID = orderID;

                db.OrderDetails.UpdateRange(details);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}
