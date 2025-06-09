using CapstoneProject_1.DTO;
using CapstoneProject_1.Models;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject_1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Athlete> Athletes { get; set; }
        public DbSet<AthletePerformanceMetric> PerformanceMetrics { get; set; }
        public DbSet<AthleteTrainingProgram> TrainingPrograms { get; set; }
        public DbSet<AthleteSubscriptionData> Subscriptions { get; set; }
        public DbSet<AthleteFeedback> Feedbacks { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<AIPrediction> AIPredictions { get; set; }
        public DbSet<PersonalizedTrainingProgram> PersonalizedTrainingPrograms { get; set; }

        public DbSet<PerformanceMetricsDTO> performanceMetricsDTOs { get; set; }

        public DbSet<AthleteSubscriptionDataDTO> AthleteSubscriptionDataDTOs { get; set; }
        public DbSet<AIPredictionDTO> AIPredictionDTOs { get; set; }
        public DbSet<AthleteFeedbackDTO> AthleteFeedbackDTOs { get; set; }
        public DbSet<AthleteTrainingProgramDTO> AthleteTrainingProgramDTOs { get; set; }
        public DbSet<Trainingprogramdto> TrainingProgramsdto { get; set; }
        public DbSet<feedbackdto> feedbackdto { get; set; }
        public DbSet<athletesubscritions> atheletesubscriptions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonalizedTrainingProgram>()
                .HasOne(p => p.Coach)
                .WithMany()
                .HasForeignKey(p => p.CoachId)
                .OnDelete(DeleteBehavior.Restrict); // Changed from Cascade

            modelBuilder.Entity<PersonalizedTrainingProgram>()
                .HasOne(p => p.Athlete)
                .WithMany()
                .HasForeignKey(p => p.AthleteId)
                .OnDelete(DeleteBehavior.Restrict); // Changed from Cascade

            modelBuilder.Entity<PersonalizedTrainingProgram>()
                .HasOne(p => p.AIPrediction)
                .WithMany()
                .HasForeignKey(p => p.AIPredictionId)
                .OnDelete(DeleteBehavior.Cascade); // Only one cascade allowed

            base.OnModelCreating(modelBuilder);
        }
    }
}
