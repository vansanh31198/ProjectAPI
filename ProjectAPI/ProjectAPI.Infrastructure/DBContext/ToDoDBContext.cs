using Microsoft.EntityFrameworkCore;
using ProjectAPI.Domain.Entities;

namespace ProjectAPI.Context
{
    public class ToDoDBContext : DbContext
    {
        public ToDoDBContext(DbContextOptions<ToDoDBContext> options) : base(options) { }

        public DbSet<Todo> Todo => Set<Todo>();
        public DbSet<Users> Users => Set<Users>();

        public DbSet<Products> Products => Set<Products>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Users>().HasKey(e => e.UserId);
            modelBuilder.Entity<Products>().HasKey(e => e.Id);
        }
    }
}
