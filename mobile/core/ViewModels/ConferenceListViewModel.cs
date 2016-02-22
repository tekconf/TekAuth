using MvvmCross.Core.ViewModels;
using System;

namespace TekConf.Mobile.Core.ViewModels
{

	public class ConferenceListViewModel : MvxViewModel
	{
		private string _slug;
		public string Slug
		{
			get { 
				return _slug;
			}
			set { 
				SetProperty (ref _slug, value);
			}
		}

		private string _name;
		public string Name
		{
			get { 
				return _name;
			}
			set { 
				SetProperty (ref _name, value);
			}
		}

		private string _description;
		public string Description
		{
			get { 
				return _description;
			}
			set { 
				SetProperty (ref _description, value);
			}
		}

		private DateTime? _startDate;
		public DateTime? StartDate
		{
			get { 
				return _startDate;
			}
			set { 
				SetProperty (ref _startDate, value);
				RaisePropertyChanged(ShortDate);
			}
		}

		private DateTime? _endDate;
		public DateTime? EndDate
		{
			get { 
				return _endDate;
			}
			set { 
				SetProperty (ref _endDate, value);
				RaisePropertyChanged(ShortDate);
			}
		}

		public string ShortDate
		{
			get { 
				return GetShortDate();
			}
		}

		private string _imageUrl;
		public string ImageUrl
		{
			get { 
				return _imageUrl;
			}
			set { 
				SetProperty (ref _imageUrl, value);
			}
		}

		private bool _isAddedToSchedule;
		public bool IsAddedToSchedule
		{
			get { 
				return _isAddedToSchedule;
			}
			set { 
				SetProperty (ref _isAddedToSchedule, value);
			}
		}

		private string _city;
		public string City
		{
			get { 
				return _city;
			}
			set { 
				SetProperty (ref _city, value);
				RaisePropertyChanged(Location);

			}
		}

		private string _stateOrProvince;
		public string StateOrProvince
		{
			get { 
				return _stateOrProvince;
			}
			set { 
				SetProperty (ref _stateOrProvince, value);
				RaisePropertyChanged(Location);
			}
		}

		private string _highlightColor;
		public string HighlightColor
		{
			get { 
				return _highlightColor;
			}
			set { 
				SetProperty (ref _highlightColor, value);
			}
		}

		public string Location
		{
			get
			{
				return GetLocation();
			}
		}


		private string GetShortDate()
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

		private string GetLocation()
		{
			if (!string.IsNullOrWhiteSpace (City) && !string.IsNullOrWhiteSpace (StateOrProvince)) {
				return string.Format ("{0}, {1}", City, StateOrProvince);
			}

			if (!string.IsNullOrWhiteSpace (City)) {
				return City;
			}

			if (!string.IsNullOrWhiteSpace (StateOrProvince)) {
				return StateOrProvince;
			}

			return "No Location Set";
		}
	}
    
}
