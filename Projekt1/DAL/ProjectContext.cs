
using Projekt1.Models;
using Projekt1.Models.Dictionary;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Projekt1.DAL
{
    public class ProjectContext : DbContext
    {

     
        public ProjectContext() : base("ProjectContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
 
        public DbSet<Specialization> Specializations { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().ToTable("Patient");
            modelBuilder.Entity<Doctor>().ToTable("Doctor");

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}