using Microsoft.EntityFrameworkCore;


namespace EducationalSeminars_4team_Novikova_Nastya
{
    public class EventDatabase : DbContext
    {
        public DbSet<Event> Events { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=\"C:\\Users\\anastasianovikova\\source\\repos\\EducationalSeminars_4team\\EducationalSeminars_4team_Novikova_Nastya\\bin\\Debug\\EventDatabase.sqlite\"");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .ToTable("Events")
                .HasKey(e => e.ID);

            modelBuilder.Entity<Event>()
                .Property(e => e.Title)
                .IsRequired();
        }
    }
}
