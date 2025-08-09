using Gutenburg_Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Gutenburg_Server.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<MeetingRequest> MeetingRequests { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Content> Contents { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Job>()
                .HasOne(j => j.User)
                .WithMany(u => u.Jobs)
                .HasForeignKey(j => j.UserId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Application>()
                .HasOne(a => a.Job)
                .WithMany(j => j.Applications)
                .HasForeignKey(a => a.JobId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);
          
            modelBuilder.Entity<User>()
    .Property(u => u.Role)
    .HasConversion<string>();

            modelBuilder.Entity<Application>()
                .Property(a => a.ApplicationStatus)
                .HasConversion<string>();

            modelBuilder.Entity<MeetingRequest>()
                .Property(m => m.MeetingStatus)
                .HasConversion<string>();

            modelBuilder.Entity<Content>()
                .Property(c => c.PageType)
                .HasConversion<string>();

            modelBuilder.Entity<Solution>()
                .Property(s => s.SolutionType)
                .HasConversion<string>();

        }


    }
}
