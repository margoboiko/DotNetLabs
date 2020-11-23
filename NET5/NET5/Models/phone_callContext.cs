using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NET5.Models
{
    public partial class phone_callContext : DbContext
    {
        public phone_callContext()
        {
        }

        public phone_callContext(DbContextOptions<phone_callContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Calling> Calling { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Person> Person { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calling>(entity =>
            {
                entity.ToTable("calling");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cityid).HasColumnName("cityid");

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasColumnType("date");

                entity.Property(e => e.During)
                    .HasColumnName("during")
                    .HasMaxLength(50);

                entity.Property(e => e.Personid).HasColumnName("personid");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Calling)
                    .HasForeignKey(d => d.Cityid)
                    .HasConstraintName("calling_cityid_fkey");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Calling)
                    .HasForeignKey(d => d.Personid)
                    .HasConstraintName("calling_personid_fkey");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("city");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City1)
                    .HasColumnName("city")
                    .HasMaxLength(50);

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Tariff)
                    .HasColumnName("tariff")
                    .HasColumnType("numeric(4,0)");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Adress)
                    .HasColumnName("adress")
                    .HasMaxLength(50);

                entity.Property(e => e.Fname)
                    .HasColumnName("fname")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.Surname)
                    .HasColumnName("surname")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
