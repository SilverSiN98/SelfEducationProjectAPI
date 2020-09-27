using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SelfEducationProjectAPI.Entities;
using SelfEducationProjectAPI.ViewModels;

namespace SelfEducationProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger _logger;
        private OrdersDbContext db;
        public ProductsController(OrdersDbContext context, ILogger<ProductsController> logger)
        {
            db = context;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<ProductVM> Get()
        {
            try
            {
                var products = from p in db.Products.ToList()
                               select new ProductVM
                               {
                                   Id = p.Product_ID,
                                   Name = p.ProductName,
                                   Price = p.ProductPrice
                               };
                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new List<ProductVM>();
            }
        }
    }
}
