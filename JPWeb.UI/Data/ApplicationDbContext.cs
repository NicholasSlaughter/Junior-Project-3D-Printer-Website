using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JPWeb.UI.Data.Model;

namespace JPWeb.UI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<JPWeb.UI.Data.Model.Request> Requests { get; set; }
        public DbSet<Printer> Printers { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }
}
