using Data.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class DataContext : DbContext
    {
        public static string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>().HasKey(x => x.id);
            modelBuilder.Entity<Clientes>(e => {
                e.HasKey(x => x.id);
                e.Property(x => x.id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Vendedores>().HasKey(x => x.id);
            modelBuilder.Entity<Vendedores>(e => {
                e.HasKey(x => x.id);
                e.Property(x => x.id).ValueGeneratedOnAdd();
            });

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Clientes> clientes { get; set; }

        public virtual DbSet<Vendedores> vendedores { get; set; }

    }
}
