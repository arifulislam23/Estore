using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Estore.Areas.Admin.Models;

namespace Estore.Data
{
    public class EstoreContext : IdentityDbContext<IdentityUser>
    {
        public EstoreContext(DbContextOptions<EstoreContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
           
        }
        public DbSet<Estore.Areas.Admin.Models.Product> Product { get; set; } = default!;
        public DbSet<Estore.Areas.Admin.Models.Label> Label { get; set; } = default!;

        //new model add here

    }
}
