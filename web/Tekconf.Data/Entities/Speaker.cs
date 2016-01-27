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

        public string ImageUrl { get; set; }
        public string EmailAddress { get; set; }
        public string TwitterHandle { get; set; }
        public string CompanyName { get; set; }
        public string LinkedInUrl { get; set; }
        public string GithubUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}