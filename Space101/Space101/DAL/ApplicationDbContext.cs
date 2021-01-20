using Microsoft.AspNet.Identity.EntityFramework;
using Space101.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Space101.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Climate> Climates { get; set; }
        public DbSet<ClimateZone> ClimateZones { get; set; }
        public DbSet<Terrain> Terrains { get; set; }
        public DbSet<SurfaceMorphology> SurfaceMorphologies { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<RaceClassification> RaceClassifications { get; set; }
        public DbSet<RaceHabitat> RaceHabitats { get; set; }
        public DbSet<Starship> Starships { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightSeat> FlightSeats { get; set; }
        public DbSet<TravelClass> TravelClasses { get; set; }
        public DbSet<FlightStatus> FlightStatuses { get; set; }
        public DbSet<FlightPath> FlightPaths { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<TicketOrder> TicketOrders { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<UserFavorite> UserFavorites { get; set; }

        public ApplicationDbContext()
            : base("Space101Context", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlightPath>()
                .HasRequired(r => r.Destination)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FlightPath>()
                .HasRequired(r => r.Departure)
                .WithMany(p => p.Destinations)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Ticket>()
                .HasRequired(o => o.PassengerPlanet)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ticket>()
               .HasRequired(o => o.PassengerRace)
               .WithMany()
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ticket>()
               .HasRequired(o => o.Seat)
               .WithMany()
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ticket>()
              .HasRequired(o => o.Flight)
              .WithMany()
              .WillCascadeOnDelete(false);

            //modelBuilder.Entity<ApplicationUser>()
            //    .HasRequired(a => a.Planet)
            //    .WithMany(p => p.ApplicationUsers)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<ApplicationUser>()
            //    .HasRequired(a => a.Race)
            //    .WithMany(r => r.ApplicationUsers)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<ApplicationUser>()
            //    .Property(a => a.RaceID)
            //    .IsOptional();

            //modelBuilder.Entity<ApplicationUser>()
            //    .Property(a => a.PlanetID)
            //    .IsOptional();

            base.OnModelCreating(modelBuilder);
        }

    }
}