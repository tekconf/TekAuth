using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tekconf.Data.Entities
{
    [Table("Sessions")]
    public class Session
    {
        public Session()
        {
            this.Speakers = new HashSet<Speaker>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Index("IX_SessionSlug", 1, IsUnique = true)]
        public string Slug { get; set; }

        [Required]
        [Index]
        [StringLength(400)]
        public string Title { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        public string Description { get; set; }

        [StringLength(100)]
        public string Room { get; set; }

        public int ConferenceId { get; set; }
        public Conference Conference { get; set; }

        public virtual ICollection<Speaker> Speakers { get; set; }

    }
}