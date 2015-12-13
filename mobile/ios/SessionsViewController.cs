using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;
using System.Linq;

namespace ios
{
	partial class SessionsViewController : UITableViewController
	{
		List<string> Sessions = new List<string> () { "1", "2", "3"};
		public SessionsViewController (IntPtr handle) : base (handle)
		{
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			return Sessions.Count ();
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("sessionCell");
			cell.TextLabel.Text = Sessions.ToArray () [indexPath.Row];

			return cell;

		}
	}
}
