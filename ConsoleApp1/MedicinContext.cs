using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class MedicinContext : DbContext
    {
        public MedicinContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public MedicinContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-7O1GS44\\SQLEXPRESS;Initial Catalog=Medicin;Integrated Security=True;Trust Server Certificate=True");
            //optionsBuilder.UseSqlServer("Data Source=LocalHost\\SQLEXPRESS;Initial Catalog=Medicin;Integrated Security=True;Trust Server Certificate=True");
        }

        internal DbSet<Apotek> Apoteker { get; set; }
        internal DbSet<Lægehus> Lægehuse { get; set; }
        public DbSet<Ordination> Ordinationer {  get; set; }
        public DbSet<Patient> Patienter { get; set; }
        public DbSet<Recept> Recepter {  get; set; }
    }
}
