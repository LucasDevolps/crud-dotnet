using EstoqueApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApi.Data
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        public void SeedUsers()
        {
            if (!Users.Any())
            {
                var password = "123";
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                var userUuid = Guid.NewGuid();

                Users.Add(new User
                {
                    Uuid = userUuid,
                    Email = "teste@teste.com",
                    PasswordHash = hashedPassword
                });

                SaveChanges();
            }
        }
    }
}
