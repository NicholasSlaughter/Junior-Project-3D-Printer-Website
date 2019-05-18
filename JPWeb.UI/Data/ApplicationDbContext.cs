using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JPWeb.UI.Data.Model;
using System.Threading.Tasks;
using System.IO;

namespace JPWeb.UI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<JPWeb.UI.Data.Model.Request> Request { get; set; }
        public DbSet<Printer> Printer { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Color> PrintColor { get; set; }

        public DbSet<JPWeb.UI.Data.Model.Message> Message { get; set; }

        public async virtual Task<Request> GetRequestById(string id)
        {
            if (id.Equals(null)) return null;

            var request = await Request.FindAsync(id);

            return request;
        }
    }
}
