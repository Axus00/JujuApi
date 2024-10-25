using Microsoft.EntityFrameworkCore;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Infrastructure.ExternalServices
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options){}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //customer
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerId);

            //post
            modelBuilder.Entity<Post>()
                .HasKey(p => p.PostId);

            
        }

        //Models
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Post> Post { get; set; }
    }
}