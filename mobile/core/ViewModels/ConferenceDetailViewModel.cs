using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;
using Refit;
using System.Net.Http;
using System;
using System.Collections.ObjectModel;
using Tekconf.DTO;

namespace TekConf.Mobile.Core.ViewModel
{
	public class ConferenceDetailViewModel : ViewModelBase
	{
		

		public ConferenceDetailViewModel (Conference conference)
		{
			Conference = conference;

			Name = conference.Name;
			Description = conference.Description;
			StartDate = conference.StartDate.Value;
			EndDate = conference.EndDate;
		}

		public Conference Conference { get; private set;}

		public string DateRange
		{
			get
			{

				string range;
				if (StartDate == default(DateTime?) || EndDate == default(DateTime?))
				{
					range = "No Date Set";
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
		private string _name;
		public string Name {
			get {
				return _name;
			}
			set {
				if (value == _name) return;
				_name = value;
				RaisePropertyChanged (() => Name);
			}
		}

		private string _description;
		public string Description {
			get {
				return _description;
			}
			set {
				if (value == _name) return;
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

	}
}