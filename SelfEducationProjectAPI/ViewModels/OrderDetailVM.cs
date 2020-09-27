using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfEducationProjectAPI.ViewModels
{
    public class OrderDetailVM
    {
        public int ItemNum { get; set; }

        public string ProductName { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
