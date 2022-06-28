using Microsoft.EntityFrameworkCore;
using RickNMortyApp.Models;

namespace RickNMortyApp.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }
        public DbSet<Models.Results> CharacterTable { get; set; }
        public DbSet<Origin> Origin { get; set; }

        public DbSet<Location> Location { get; set; }

        public DbSet<Episode> Episode { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Results>(entity =>
        {
            entity.Property(e => e.Id)
           .HasMaxLength(1000)
           .IsUnicode(false);

            entity.Property(e => e.Name)
           .HasMaxLength(1000)
           .IsUnicode(false);

            entity.Property(e => e.Status)
           .HasMaxLength(1000)
           .IsUnicode(false);

            entity.Property(e => e.Species)
           .HasMaxLength(1000)
           .IsUnicode(false);

            entity.Property(e => e.Type)
           .HasMaxLength(1000)
           .IsUnicode(false);

            entity.Property(e => e.Gender)
           .HasMaxLength(1000)
           .IsUnicode(false);

            entity.Property(e => e.Url)
           .HasMaxLength(1000)
           .IsUnicode(false);
            entity.Property(e => e.Created)
           .HasMaxLength(1000)
           .IsUnicode(false);
            
        });
            modelBuilder.Entity<Origin>(entity =>
            {
                entity.Property(e => e.Id)
                .HasMaxLength(1000)
                .IsUnicode(false);

                entity.Property(e => e.Name)
                .HasMaxLength(1000)
                .IsUnicode(false);
            });
            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Id)
                .HasMaxLength(1000)
                .IsUnicode(false);

                entity.Property(e => e.Name)
                .HasMaxLength(1000)
                .IsUnicode(false);
            });
            modelBuilder.Entity<Episode>(entity =>
            {
                entity.Property(e => e.Id)
                .HasMaxLength(1000)
                .IsUnicode(false);

                entity.Property(e => e.Url)
                .HasMaxLength(1000)
                .IsUnicode(false);
            });
            modelBuilder.Entity<Models.Results>().ToTable("character");
            modelBuilder.Entity<Origin>().ToTable("origin");
            modelBuilder.Entity<Location>().ToTable("location");
            modelBuilder.Entity<Episode>().ToTable("episode");
        }


    }
}
