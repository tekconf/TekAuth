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
        [StringLength(100)]
        public string Slug { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        public string Bio { get; set; }


        [StringLength(100)]
        public string JobTitle { get; set; }

        [StringLength(100)]
        public string EmailAddress { get; set; }
        [StringLength(50)]
        public string TwitterHandle { get; set; }
        [StringLength(100)]
        public string CompanyName { get; set; }
        [StringLength(20)]
        public string PhoneNumber { get; set; }


        [StringLength(2083)]
        public string ImageUrl { get; set; }

        [StringLength(2083)]
        public string WebsiteUrl { get; set; }

        [StringLength(2083)]
        public string LinkedInUrl { get; set; }
        [StringLength(2083)]
        public string GithubUrl { get; set; }
        [StringLength(2083)]
        public string FacebookUrl { get; set; }


        public virtual ICollection<Session> Sessions { get; set; }
    }
}