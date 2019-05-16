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
    public class ApplicationDbContext : IdentityDbContext<AccountController>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<JPWeb.UI.Data.Model.Request> Requests { get; set; }
        public DbSet<Printer> Printers { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public DbSet<JPWeb.UI.Data.Model.Message> Messages { get; set; }

        public async virtual Task<Request> GetRequestById(int? id)
        {
            if (id == null) return null;

            var request = await Requests.FindAsync(id);

            return request;
        }
    }
}
