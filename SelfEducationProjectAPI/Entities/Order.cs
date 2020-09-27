using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfEducationProjectAPI.Entities
{
    public class Order
    {
        public int Order_ID { get; set; }

        //public string ProductNames { get; set; }

        //public decimal TotalPrice { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
