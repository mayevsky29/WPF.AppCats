using Microsoft.EntityFrameworkCore;
using CatRenta.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatRenta.EFData
{
    public class DataContext : DbContext 
    {
        public DbSet<AppCat> Cats { get; set; }
        public DbSet<AppCatPrice> CatPrices { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=91.238.103.51;Port=5743;Database=mayevskydb;Username=maey;Password=$544$B77w**G)K$t!Ube22}mav");
        }
    }
}
