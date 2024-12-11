using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shlyapnikova_lr.Models;

namespace Shlyapnikova_lr.Data
{
    public class Shlyapnikova_lrContext : DbContext
    {
        public Shlyapnikova_lrContext (DbContextOptions<Shlyapnikova_lrContext> options)
            : base(options)
        {
        }

        public DbSet<Shlyapnikova_lr.Models.Student> Student { get; set; } = default!;
        public DbSet<Shlyapnikova_lr.Models.Volunteer> Volunteer { get; set; } = default!;
    }
}
