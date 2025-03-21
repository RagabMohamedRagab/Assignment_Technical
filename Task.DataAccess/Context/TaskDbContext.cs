using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task.Core.Entities;

namespace Task.DataAccess.Context
{
    public class TaskDbContext : IdentityDbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.Entity<Product>().HasIndex(b => b.Code).IsUnique();
        }


        public virtual DbSet<Product> Products { get; set; }
    }
}
