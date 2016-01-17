using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tekconf.Data.Migrations;

namespace Tekconf.Data.Entities
{
    [Table("Speakers")]
    public class Speaker
    {
        public Speaker()
        {
            this.Sessions = new HashSet<Session>();
        }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}