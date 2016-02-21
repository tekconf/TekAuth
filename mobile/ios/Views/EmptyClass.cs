﻿
using System;
using System.Collections.Generic;
using MvvmCross.iOS.Views;
using UIKit;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using TekConf.Mobile.Core.ViewModels;
using TekConf.Mobile.Core;

namespace ios
{
	public class PolymorphicListItemTypesView
		: MvxTableViewController
	{
		public PolymorphicListItemTypesView()
		{
			Title = "Poly List";
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			//var cell = TableView.DequeueReusableCell("KittenCell");

			var source = new TableSource(TableView);
			this.AddBindings(new Dictionary<object, string>
			{
				{source, "ItemsSource Animals"}
			});
			TableView.Source = source;
			TableView.ReloadData();
		}

		public class TableSource : MvxTableViewSource
		{
			private static readonly NSString KittenCellIdentifier = new NSString("KittenCell");
			private static readonly NSString DogCellIdentifier = new NSString("DogCell");

			public TableSource(UITableView tableView)
				: base(tableView)
			{
				tableView.RegisterNibForCellReuse(UINib.FromName("ConfListCell", NSBundle.MainBundle),
				                                                  ConfListCell.Key);
				
				tableView.RegisterNibForCellReuse(UINib.FromName("KittenCell", NSBundle.MainBundle),
				                                  KittenCellIdentifier);
				tableView.RegisterNibForCellReuse(UINib.FromName("DogCell", NSBundle.MainBundle), DogCellIdentifier);
			}

			protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath,
			                                                      object item)
			{
				NSString cellIdentifier;
				if (item is Kitten)
				{
					cellIdentifier = KittenCellIdentifier;
				}
				else if (item is Dog)
				{
					cellIdentifier = DogCellIdentifier;
				}
				else
				{
					throw new ArgumentException("Unknown animal of type " + item.GetType().Name);
				}
				cellIdentifier = ConfListCell.Key;
				return (UITableViewCell)TableView.DequeueReusableCell(cellIdentifier, indexPath);
			}
		}
	}
}