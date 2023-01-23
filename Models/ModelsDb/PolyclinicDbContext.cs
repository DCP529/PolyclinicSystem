using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Services;
using System.Configuration;

namespace Models.ModelsDb
{
    public class PolyclinicDbContext : DbContext
    {
        public DbSet<AccountDb> Accounts { get; set; }
        public DbSet<RoleDb> Roles { get; set; }
        public DbSet<LoginDb> Logins { get; set; }
        public DbSet<CityDb> Cities { get; set; }
        public DbSet<PolyclinicDb> Polyclinics { get; set; }
        public DbSet<DoctorDb> Doctors { get; set; }
        public DbSet<SpecializationDb> Specializations { get; set; }

        public PolyclinicDbContext(DbContextOptions<PolyclinicDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var faker = new DataGenerator();

            var polyclinics = faker.PolyclinicsGenerate(15);
            var cities = faker.CitiesGenerate(15);
            var doctors = faker.DoctorsGenerate(15);
            var specializations = faker.SpecializationsGenerate(15);

            modelBuilder.Entity<PolyclinicDb>().HasData(polyclinics);
            modelBuilder.Entity<CityDb>().HasData(cities);
            modelBuilder.Entity<DoctorDb>().HasData(doctors);
            modelBuilder.Entity<SpecializationDb>().HasData(specializations);
        }

    }
}
