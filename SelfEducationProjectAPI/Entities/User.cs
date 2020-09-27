using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfEducationProjectAPI.Entities
{
    public class User
    {
        public int User_ID { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string Password { get; set; }
    }
}
