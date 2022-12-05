﻿using Microsoft.EntityFrameworkCore;

namespace Models.ModelsDb
{
    public class PolyclinicDbContext : DbContext
    {
        public DbSet<CityDb> Cities { get; set; }
        public DbSet<PolyclinicDb> Polyclinics { get; set; }
        public DbSet<DoctorDb> Doctors { get; set; }
        public DbSet<SpecializationDb> Specializations { get; set;  }

        public PolyclinicDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseNpgsql("Host=localhost;" +
                "Port=5433;" +
                "Database=PolyclinicSystem;" +
                "Username=postgres;" +
                "Password=super200;");
        }
    }
}