using Microsoft.EntityFrameworkCore;
using Myshowroom.Models;
using TechLearn.Models.Domain_Models;

namespace Myshowroom.DataContext
{
    public class dataContext : DbContext
    {
        public dataContext(DbContextOptions<dataContext> options) : base(options) { }

        public DbSet<Notes> LearningNotes { get; set; }
        public virtual DbSet<ProgrammingLanguages> ProgrammingLanguages { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<StudentResponse> StudentResponses { get; set; }
        public DbSet<QuizResult> QuizResults { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notes>()
                .HasKey(n => n.Id);

            modelBuilder.Entity<ProgrammingLanguages>()
                .HasKey(pl => pl.Id);

            modelBuilder.Entity<Assessment>()
           .HasMany(a => a.Options)
           .WithOne(o => o.Assessment)
           .HasForeignKey(o => o.AssessmentId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
