using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_core.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<Category> categories { get; set; }
        public DbSet<Skill> skills { get; set; }
    }
}