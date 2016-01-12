using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tekconf.Data.Entities
{
    [Table("Schedules")]
    public class Schedule
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int ConferenceId { get; set; }
        public virtual Conference Conference { get; set; }

        public DateTime Created { get; set; }
    }
}