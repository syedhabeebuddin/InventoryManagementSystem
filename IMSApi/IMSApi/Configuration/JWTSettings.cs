using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSApi.Configuration
{
    public class JWTSettings
    {
        public string SecretKey { get; set; }
        public int ExpirationInDays { get; set; }
    }
}
