using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace Tekconf.Data.Entities
{
    using System.Data.Entity;

    public partial class ConferenceContext : DbContext
    {
        public ConferenceContext()
            : base("name=ConferenceContext")
        {
        }

        public virtual DbSet<Conference> Conferences { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conference>()
                .Property(c => c.Latitude).HasPrecision(12, 10);

            modelBuilder.Entity<Conference>()
                .Property(c => c.Longitude).HasPrecision(12, 10);

            modelBuilder.Entity<User>()
                   .HasMany(u => u.Conferences)
                   .WithMany(c => c.Users)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("UserId");
                       cs.MapRightKey("ConferenceId");
                       cs.ToTable("UserConferences");
                   });

            modelBuilder.Entity<Speaker>()
               .HasMany(u => u.Sessions)
               .WithMany(s => s.Speakers)
               .Map(cs =>
               {
                   cs.MapLeftKey("SpeakerId");
                   cs.MapRightKey("SessionId");
                   cs.ToTable("SessionSpeakers");
               });

            modelBuilder.Entity<Schedule>()
                .HasRequired(t => t.Conference)
                .WithMany()
                .HasForeignKey(t => t.ConferenceId);

            modelBuilder.Entity<Schedule>()
                .HasRequired(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId);

            modelBuilder
                .Entity<Schedule>()
                .Property(t => t.UserId)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_UserIdConferenceId", 1) { IsUnique = true }));

            modelBuilder
                .Entity<Schedule>()
                .Property(t => t.ConferenceId)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_UserIdConferenceId", 2) { IsUnique = true }));


            modelBuilder.Entity<Session>().HasRequired(p => p.Conference);

        }
    }
}