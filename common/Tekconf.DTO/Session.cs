using System;

namespace Tekconf.DTO
{
	public class Session
	{
		//public int Id { get; set; }

		public string Slug { get; set; }

		public string Title { get; set; }

		public DateTime? StartDate { get; set; }

		public DateTime? EndDate { get; set; }

		public string SpeakerName { get; set; }

		public string Description { get; set; }

		public string Room { get; set; }
	}
    
}
