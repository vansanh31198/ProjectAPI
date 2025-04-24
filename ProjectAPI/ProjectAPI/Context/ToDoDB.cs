using Microsoft.EntityFrameworkCore;
using ProjectAPI.Model;

namespace ProjectAPI.Context
{
    public class ToDoDB : DbContext
    {
        public ToDoDB(DbContextOptions<ToDoDB> options) : base(options) { }

        public DbSet<Todo> Todos => Set<Todo>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
