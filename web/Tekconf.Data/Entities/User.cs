using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tekconf.Data.Entities
{
    [Table("Users")]
    public class User
    {
        public User()
        {
            this.Conferences = new HashSet<Conference>();
        }
        public int Id { get; set; }

        [Required]
        [Index]
        [StringLength(500)]
        public string Name { get; set; }

        public virtual ICollection<Conference> Conferences { get; set; }
    }
}