using GalaSoft.MvvmLight;
using System;
using Tekconf.DTO;

namespace TekConf.Mobile.Core.ViewModels
{
	public class SessionDetailViewModel : ViewModelBase
	{
		public SessionDetailViewModel (Session session, string conferenceName)
		{
			Session = session;

			ConferenceName = conferenceName;
			Title = Session.Title;
			Description = Session.Description;
			StartDate = Session.StartDate;
			EndDate = Session.EndDate;
			Room = Session.Room;
		}

		public Session Session { get; private set;}

		private string _conferenceName;
		public string ConferenceName {
			get {
				return _conferenceName;
			}
			set {
				if (value == _conferenceName) return;
				_conferenceName = value;
				RaisePropertyChanged (() => ConferenceName);
			}
		}

		private string _title;
		public string Title {
			get {
				return _title;
			}
			set {
				if (value == _title) return;
				_title = value;
				RaisePropertyChanged (() => Title);
			}
		}

		private string _room;
		public string Room {
			get {
				return _room;
			}
			set {
				if (value == _room) return;
				_room = value;
				RaisePropertyChanged (() => Room);
			}
		}

		private string _description;
		public string Description {
			get {
				return _description;
			}
			set {
				if (value == _description) return;
				_description = value;
				RaisePropertyChanged (() => Description);
			}
		}

		private DateTime? _startDate;
		public DateTime? StartDate {
			get {
				return _startDate;
			}
			set {
				if (value == _startDate) return;
				_startDate = value;
				RaisePropertyChanged (() => StartDate);
			}
		}

		private DateTime? _endDate;
		public DateTime? EndDate {
			get {
				return _endDate;
			}
			set {
				if (value == _endDate) return;
				_endDate = value;
				RaisePropertyChanged (() => EndDate);
			}
		}

		public string DateRange
		{
			get
			{

				string range;
				if (StartDate == default(DateTime?) && EndDate == default(DateTime?)) {
					range = "No Date Set";
				} else if (StartDate.HasValue && !EndDate.HasValue) {
					// Only start Date
					range = StartDate.Value.ToString("MMMM") + " " + StartDate.Value.Day + ", " + StartDate.Value.Year;
				}
				else if (StartDate.Value.Month == EndDate.Value.Month && StartDate.Value.Year == EndDate.Value.Year)
				{
					// They begin and end in the same month
					if (StartDate.Value.Date == EndDate.Value.Date)
					{
						range = StartDate.Value.ToString("MMMM") + " " + StartDate.Value.Day + ", " + StartDate.Value.Year;
					}
					else
						range = StartDate.Value.ToString("MMMM") + " " + StartDate.Value.Day + " - " + EndDate.Value.Day + ", " + StartDate.Value.Year;
				}
				else
				{
					// They begin and end in different months
					if (StartDate.Value.Year == EndDate.Value.Year)
					{
						range = StartDate.Value.ToString("MMMM") + " " + StartDate.Value.Day + " - " + EndDate.Value.ToString("MMMM") + " " + EndDate.Value.Day + ", " + StartDate.Value.Year;
					}
					else
					{
						range = StartDate.Value.ToString("MMMM") + " " + StartDate.Value.Day + ", " + StartDate.Value.Year + " - " + EndDate.Value.ToString("MMMM") + " " + EndDate.Value.Day + ", " + EndDate.Value.Year;
					}

				}

				return range;
			}
		}
	}
	
}