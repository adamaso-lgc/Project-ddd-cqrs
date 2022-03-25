using Microsoft.EntityFrameworkCore;
using Project.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infra.Database.Context
{
    public sealed class ProjectContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectContext).Assembly);
        }
    }
}
