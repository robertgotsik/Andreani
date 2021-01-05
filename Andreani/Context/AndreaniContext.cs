using Andreani.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Andreani.Context
{
    public class AndreaniContext:DbContext
    {
        public AndreaniContext(DbContextOptions<AndreaniContext> options) : base(options)
        {
        }
        //public DbSet<Geocodificacion> Geocodificacion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Geolocalizacion>()
                .Property(b => b.Estado)
                .HasDefaultValue(0);
        }

        public DbSet<Andreani.Models.Geolocalizacion> Geolocalizacion { get; set; }
    }
}
