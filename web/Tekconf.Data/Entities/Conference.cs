using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity.Spatial;

namespace Tekconf.Data.Entities
{
    [Table("Conferences")]
    public class Conference
    {
        public int Id { get; set; }

        public Conference()
        {
            this.Users = new HashSet<User>();
            this.Sessions = new HashSet<Session>();

        }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }

        [Required]
        [Index("IX_ConferenceSlug", 1, IsUnique = true)]
        [StringLength(100)]
        public string Slug { get; set; }
        
        [Required]
        [Index]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(8000)]
        public string Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        public bool IsLive { get; set; }

        [Column(TypeName = "date")]
        public DateTime DatePublished { get; set; }

        public int? DefaultSessionLength { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CallForSpeakersOpen { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CallForSpeakersCloses { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RegistrationOpens { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RegistrationCloses { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateAdded { get; set; }

        [Column(TypeName = "date")]
        public DateTime LastUpdated { get; set; }

        public bool IsOnlineConference { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string StateOrProvince { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string VenuePhoneNumber { get; set; }
        public string OrganizerPhoneNumber { get; set; }

        public string ImageUrl { get; set; }
        public string ImageSquareUrl { get; set; }
        public string TagLine { get; set; }
        public string FacebookUrl { get; set; }
        public string HomepageUrl { get; set; }
        public string LanyrdUrl { get; set; }

        public string TwitterHashtag { get; set; }
        public string TwitterName { get; set; }
        public string MeetupUrl { get; set; }
        public string GooglePlusUrl { get; set; }
        public string VimeoUrl { get; set; }
        public string YouTubeUrl { get; set; }
        public string GithubUrl { get; set; }
        public string LinkedInUrl { get; set; }

        [MaxLength(10)]
        public string HighlightColor { get; set; }

    }

    //[Table("Users")]
    //public class User
    //{
    //    public int Id { get; set; }
    //    public 
    //}
}
