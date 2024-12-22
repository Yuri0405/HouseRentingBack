using System;
using System.Collections.Generic;

namespace ClientService.Models
{
    public partial class User
    {
        public string Id { get; set; }
        public string? Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? Password { get; set; } = null!;
    }
}
