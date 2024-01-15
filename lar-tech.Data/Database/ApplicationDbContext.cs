using lar_tech.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace lar_tech.Data.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Person> People { get; set; }
        private IConfiguration _configuration { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        //Configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasAlternateKey(p => p.Cpf);

            modelBuilder.Entity<Person>()
                .HasMany(p => p.PhoneNumbers)
                .WithOne()
                .HasForeignKey(p => p.PersonId)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
