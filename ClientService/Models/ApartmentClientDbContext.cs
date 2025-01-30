using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ClientService.Models
{
    public partial class ApartmentClientDbContext : IdentityDbContext<AppUser>
    {
        public ApartmentClientDbContext()
        {
        }

        public ApartmentClientDbContext(DbContextOptions<ApartmentClientDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);
        }

    }
}
