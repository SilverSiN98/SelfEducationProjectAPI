using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfEducationProjectAPI.Authentication
{
    public class AuthOptions
    {
        public const string Issuer = "MyAuthServer"; 
        public const string Audience = "AngularClient";
        const string Key = "mysecretkeyforangularclient";
        public const int Lifetime = 1;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
