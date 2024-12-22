using System;
using System.Collections.Generic;

namespace ApartmentService.Models
{
    public partial class Apartment
    {
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string ApartmentNumber { get; set; } = null!;
        public string BuildingNumber { get; set; } = null!;
        public int NumberOfRooms { get; set; }
        public Guid Id { get; set; }
        public bool IsRented { get; set; }
        public Guid OwnerId { get; set; }
    }
}
