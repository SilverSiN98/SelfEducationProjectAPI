﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfEducationProjectAPI.Entities
{
    public class OrderDetail
    {
        public int OrderDetail_ID { get; set; }

        public int? OrderID { get; set; }

        public int ProductID { get; set; }

        //public string OrderDetailName { get; set; }

        public decimal TotalPrice { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}
