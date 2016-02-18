using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using TekConf.Mobile.Core.ViewModels;
using System.Collections.Generic;
using MvvmCross.Binding.iOS.Views;
using Foundation;
using UIKit;
using Tekconf.DTO;
using System;
using System.Collections.ObjectModel;

namespace ios.Views
{
	public class ConferencesView : MvxTableViewController
	{
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            Title = "Conferences";

            this.TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			var source = new MvxSimpleTableViewSource(TableView, ConferenceCell.Key, ConferenceCell.Key);
			TableView.Source = source;
			var set = this.CreateBindingSet<ConferencesView, ConferencesViewModel>();
			set.Bind(source).To(vm => vm.Conferences);

            set.Bind(source)
               .For(s => s.SelectionChangedCommand)
               .To(vm => vm.ShowConference);
            set.Apply();

			var confVm = (ConferencesViewModel)this.ViewModel;
			confVm.Conferences = new ObservableCollection<Conference> (
				new List<Conference> () {
					new Conference {
						Name = "Codemash",
						Slug = "codemash-2016",
						Description = ""
					}
				}
			);
			TableView.ReloadData ();
		}

	}
    
}
