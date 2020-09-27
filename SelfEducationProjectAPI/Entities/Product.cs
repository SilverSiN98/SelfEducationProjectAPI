using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfEducationProjectAPI.Entities
{
    public class Product
    {
        public int Product_ID { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
