namespace Tekconf.Data.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ConferenceContext : DbContext
    {
        public ConferenceContext()
            : base("name=ConferenceContext")
        {
        }

        public virtual DbSet<Conference> Conferences { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            


        }
    }
}
