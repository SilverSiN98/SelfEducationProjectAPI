﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfEducationProjectAPI.ViewModels
{
    public class UserToken
    {
        public string Token { get; set; }

        public string UserName { get; set; }

        public int Lifetime { get; set; } 
    }
}
