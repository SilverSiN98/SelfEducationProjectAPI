using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfEducationProjectAPI.ViewModels
{
    public class OrderVM
    {
        public int OrderNum { get; set; }

        public string ProductNames { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
