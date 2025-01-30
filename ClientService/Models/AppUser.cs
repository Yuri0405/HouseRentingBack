using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ClientService.Models
{
    public partial class AppUser:IdentityUser
    {
        //public string Id { get; set; }
        public string? Name { get; set; } = null!;
        //public string? Email { get; set; }
        public string? Password { get; set; } = null!;
    }
}
