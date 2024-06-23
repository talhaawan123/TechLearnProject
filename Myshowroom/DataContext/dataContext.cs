using Microsoft.EntityFrameworkCore;
using Myshowroom.Models;
using TechLearn.Models.Domain_Models;

namespace Myshowroom.DataContext
{
    public class dataContext : DbContext
    {
        public dataContext(DbContextOptions<dataContext> options) : base(options) { }

        public DbSet<Notes> LearningNotes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public virtual DbSet<ProgrammingLanguages> ProgrammingLanguages { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Notes>()
                .HasKey(n => n.Id);

            //modelBuilder.Entity<ProgrammingLanguages>()
            //    .HasMany(pl => pl.Notes)
            //    .WithOne(n => n.ProgrammingLanguage)
            //    .HasForeignKey(n => n.ProgrammingLanguageId);

            //base.OnModelCreating(modelBuilder);
        }
    }
}
