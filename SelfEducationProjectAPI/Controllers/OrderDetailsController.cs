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
    public class OrderDetailsController : ControllerBase
    {
        private readonly ILogger _logger;
        private OrdersDbContext db;
        public OrderDetailsController(OrdersDbContext context, ILogger<OrderDetailsController> logger)
        {
            db = context;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<OrderDetailVM> Get()
        {
            try
            {
                var orderDetails = from od in db.OrderDetails
                                   where od.OrderID == null
                                   select new OrderDetailVM
                                   {
                                       ItemNum = od.OrderDetail_ID,
                                       ProductName = od.Product.ProductName,
                                       TotalPrice = od.TotalPrice
                                   };

                return orderDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new List<OrderDetailVM>();
            }
        }

        [Authorize]
        [HttpPost]
        public bool Post([FromBody]OrderDetail orderDetail)
        {
            try
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            try
            {
                db.OrderDetails.Remove(db.OrderDetails.Where(x => x.OrderDetail_ID == id).First());
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
