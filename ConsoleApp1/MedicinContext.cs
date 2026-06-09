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
            Seed();

        }

        /*public MedicinContext()
        {
            Database.EnsureCreated();
            Seed();
        }*/ // Behøver umiddelbart ikke

        private void Seed()
        {
            if (Recepter.Any() || Apoteker.Any() || Lægehuse.Any() || Lægehuse.Any() || Patienter.Any()) {
                return;
            }
            var apotek1 = new Apotek("Løve apoteket");
            var apotek2 = new Apotek("Medicinmanden");
            var apotek3 = new Apotek("Apo24/7");

            var lægehus1 = new Lægehus("E7KFT32", "Banegårdslægerne");
            var lægehus2 = new Lægehus("AB35CDE", "Lægerne i Lunderskov");
            var lægehus3 = new Lægehus("FE67KEG", "Langenæslægen");

            var patient1 = new Patient("123456781111", "Anders");
            var patient2 = new Patient("123412341111", "Peter");
            var patient3 = new Patient("876543219999", "Christina");

            var ordination1 = new Ordination("Morfin", "2 piller dagligt, morgen og aften", 8);
            var ordination2 = new Ordination("Paracetamol", "4 piller dagligt, morgen, middag, eftermiddag, aften", 4);
            var ordination3 = new Ordination("Ibuprofen", "2 piller dagligt, morgen og aften", 6);
            var ordination4 = new Ordination("Melatonin", "1 sprøjt dagligt, aften", 2);
            ordination4.AntalForetagneUdleveringer = 2;

            this.AddRange(ordination1, ordination2, ordination3, ordination4);
            this.SaveChanges();

            var recept1 = new Recept("E7KFT32", "123456781111");
            recept1.OrdinationerId.Add(ordination1.Id);
            var recept2 = new Recept("AB35CDE", "123412341111");
            recept2.OrdinationerId.Add(ordination2.Id);
            recept1.OrdinationerId.Add(ordination3.Id);
            recept1.OrdinationerId.Add(ordination4.Id);


            this.AddRange(apotek1, apotek2, apotek3, lægehus1, lægehus2, lægehus3, patient1, patient2, recept1, recept2);
            this.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-7O1GS44\\SQLEXPRESS;Initial Catalog=Medicin;Integrated Security=True;Trust Server Certificate=True");
            //optionsBuilder.UseSqlServer("Data Source=LocalHost\\SQLEXPRESS;Initial Catalog=Medicin;Integrated Security=True;Trust Server Certificate=True");
        }

        internal DbSet<Apotek> Apoteker { get; set; }
        internal DbSet<Lægehus> Lægehuse { get; set; }
        internal DbSet<Ordination> Ordinationer {  get; set; }
        internal DbSet<Patient> Patienter { get; set; }
        internal DbSet<Recept> Recepter {  get; set; }
    }
}
