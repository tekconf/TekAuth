using System;
using System.Collections.Generic;

namespace Tekconf.DTO
{
    public class Session
	{
		public string Slug { get; set; }

		public string Title { get; set; }

		public DateTime? StartDate { get; set; }

		public DateTime? EndDate { get; set; }

		public string Description { get; set; }

		public string Room { get; set; }

        public List<Speaker> Speakers { get; set; }
    }
}