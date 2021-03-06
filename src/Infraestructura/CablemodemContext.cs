﻿using Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura
{
    public class CablemodemContext : DbContext
    {
        public DbSet<Cablemodem> Cablemodem { get; set; }
        public CablemodemContext(DbContextOptions<CablemodemContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuider)
        {
            modelBuider.Entity<Cablemodem>().ToTable("docsis_update");
            modelBuider.Entity<Cablemodem>().Property(c => c.MacAddress).HasColumnName("modem_macaddr");
            modelBuider.Entity<Cablemodem>().HasKey(c => c.MacAddress);
            modelBuider.Entity<Cablemodem>().Property(c => c.Ip).HasColumnName("ipaddr");
            modelBuider.Entity<Cablemodem>().HasIndex(c => c.Ip).IsUnique();
            modelBuider.Entity<Cablemodem>().Property(c => c.Modelo).HasColumnName("vsi_model");
            modelBuider.Entity<Cablemodem>().Property(c => c.Fabricante).HasColumnName("vsi_vendor");
            modelBuider.Entity<Cablemodem>().Property(c => c.VersionSoftware).HasColumnName("vsi_swver");

            base.OnModelCreating(modelBuider);
        }
    }
}
