using Microsoft.EntityFrameworkCore;
using Dynamic_Sentence_Web_App.Models;

namespace Dynamic_Sentence_Web_App.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<WordType> WordTypes { get; set; }
        public DbSet<WordUnit> WordUnits { get; set; }
        public DbSet<Sentence> Sentences { get; set; }
        public DbSet<WordSentence> WordSentences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordType>().ToTable("WordType");
            modelBuilder.Entity<WordUnit>().ToTable("WordUnit");
            modelBuilder.Entity<Sentence>().ToTable("Sentence");
            modelBuilder.Entity<WordSentence>().ToTable("WordSentence");
            modelBuilder.Entity<WordSentence>().HasKey(ws => new { ws.WordUnitId, ws.SentenceId });
            modelBuilder.Entity<WordSentence>().HasOne(ws => ws.WordUnit).WithMany(w => w.WordSentences).HasForeignKey(ws => ws.WordUnitId);
            modelBuilder.Entity<WordSentence>().HasOne(ws => ws.Sentence).WithMany(s => s.WordSentences).HasForeignKey(ws => ws.SentenceId);
        }
    }
}
