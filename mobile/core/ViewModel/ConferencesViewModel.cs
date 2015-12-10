using GalaSoft.MvvmLight;
using PropertyChanged;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;

namespace TekConf.Mobile.Core.ViewModel
{
	public class ConferencesViewModel : ViewModelBase
	{
		public RelayCommand LoadConferencesCommand { get; private set; }
		public ConferencesViewModel ()
		{
			this.LoadConferencesCommand = new RelayCommand(this.LoadConferences, CanLoadConferences);

		}

		private async Task LoadConferences()
		{
		}

		public bool CanLoadConferences()
		{
			return true;
		}
	}

}