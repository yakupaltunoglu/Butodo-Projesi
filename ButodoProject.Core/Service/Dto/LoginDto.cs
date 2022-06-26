using System;
using System.Collections.Generic;
using System.Text;

namespace ButodoProject.Core.Service.Dto
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }

        public Guid AdminId { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }

    }
}
