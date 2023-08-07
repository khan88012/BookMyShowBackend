
using BookMovie2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
      
        public DbSet<Theatre> Theatres { get; set; }

        public DbSet<Audi> Audis { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<TheatreByLocationResponse> TheatresByLocations { get; set; }
        public DbSet<TheatreByMovieResponse> TheatresByMovies { get; set; }

        public DbSet<Category> Categories { get; set; } 

        public DbSet<User> Users { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audi>()
                .HasKey(ms => new { ms.Id });
            modelBuilder.Entity<Audi>()
                .HasOne(ms => ms.Movie)
                .WithMany(ms => ms.Theatres)
                .HasForeignKey(ms => ms.MovieId);

            modelBuilder.Entity<Audi>()
                .HasOne(ms => ms.Theatre)
                .WithMany(ms => ms.Movies)
                .HasForeignKey(ms => ms.TheatreId);
        }
    }
}
