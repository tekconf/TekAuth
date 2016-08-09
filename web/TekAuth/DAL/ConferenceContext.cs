using System.ComponentModel.DataAnnotations.Schema;
using Tekconf.Data.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using Tekconf.Data.Entities;

namespace TekAuth.DAL
{
    using System;
    using System.Data;
    using System.Threading.Tasks;

    public class ConferenceContext : DbContext
    {
        private DbContextTransaction _currentTransaction;


        public ConferenceContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        //public DbSet<Course> Courses { get; set; }
        //public DbSet<Department> Departments { get; set; }
        //public DbSet<Enrollment> Enrollments { get; set; }
        //public DbSet<Instructor> Instructors { get; set; }
        //public DbSet<Student> Students { get; set; }
        //public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        //public DbSet<Person> People { get; set; }
        //public DbSet<CourseInstructor> CourseInstructors { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        //    modelBuilder.Entity<CourseInstructor>().HasKey(ci => new { ci.CourseID, ci.InstructorID });

        //    modelBuilder.Entity<Department>().MapToStoredProcedures();
        //}

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

        public void BeginTransaction()
        {
            try
            {
                if (_currentTransaction != null)
                {
                    return;
                }

                _currentTransaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            catch (Exception)
            {
                // todo: log transaction exception
                throw;
            }
        }

        public void CloseTransaction()
        {
            CloseTransaction(exception: null);
        }

        public void CloseTransaction(Exception exception)
        {
            try
            {
                if (_currentTransaction != null && exception != null)
                {
                    // todo: log exception
                    _currentTransaction.Rollback();
                    return;
                }

                SaveChanges();

                if (_currentTransaction != null)
                {
                    _currentTransaction.Commit();
                }
            }
            catch (Exception)
            {
                // todo: log exception
                if (_currentTransaction != null && _currentTransaction.UnderlyingTransaction.Connection != null)
                {
                    _currentTransaction.Rollback();
                }

                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

    }
}