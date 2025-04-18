using Microsoft.EntityFrameworkCore;
using System;
using System.IO;


namespace EducationalSeminars_4team_Novikova_Nastya
{
    public class EventDatabase : DbContext
    {
        public DbSet<Event> Events { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EventDatabase.sqlite");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .ToTable("Events")
                .HasKey(e => e.EventId);

            modelBuilder.Entity<Event>();
                
        }

    }
}
