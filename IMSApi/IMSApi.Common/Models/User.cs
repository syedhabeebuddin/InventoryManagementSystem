using System;
using System.Collections.Generic;
using System.Text;

namespace IMSApi.Common.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UserStatus { get; set; }
        public string UserRole { get; set; }
    }
}
