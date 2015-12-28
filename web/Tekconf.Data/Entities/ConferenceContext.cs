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
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                   .HasMany<Conference>(u => u.Conferences)
                   .WithMany(c => c.Users)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("UserId");
                       cs.MapRightKey("ConferenceId");
                       cs.ToTable("UserConferences");
                   });

            modelBuilder.Entity<Session>().HasRequired(p => p.Conference);

        }
    }
}