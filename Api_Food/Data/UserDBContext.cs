using Api_Food.Models;
using Food_Users_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Food_Users_Api
{
    public class UserDBContext : DbContext
    {

        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FoodUser>()
                .HasKey(fu => new { fu.UserId, fu.FoodId });

            modelBuilder.Entity<FoodUser>()
                .HasOne(f => f.Food)
                .WithMany(fu => fu.FoodUser)
                .HasForeignKey(f => f.FoodId);
            
            modelBuilder.Entity<FoodUser>()
                .HasOne(u => u.User)
                .WithMany(fu => fu.FoodUser)
                .HasForeignKey(u => u.UserId);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; database=FOOD_APP_USER_MAIN; Trusted_Connection=true; MultipleActiveResultSets=true; Integrated Security=true;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodUser> FoodUsers { get; set; }

    }
}
