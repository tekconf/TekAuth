using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Tekconf.DTO;
using CoreGraphics;

namespace ios
{
	partial class StackCell : UITableViewCell
	{
		Session _session;
		//UIStackView _mainStackView;
		public StackCell (IntPtr handle) : base (handle)
		{
//			_mainStackView = new UIStackView { 
//				Axis = UILayoutConstraintAxis.Horizontal,
//				Distribution = UIStackViewDistribution.FillProportionally,
//				Alignment = UIStackViewAlignment.Leading,
//				Spacing = 0,
//				TranslatesAutoresizingMaskIntoConstraints = false
//			};
//
//			ContentView.Add (_mainStackView);
//
//			ContentView.AddConstraint(_mainStackView.LeftAnchor.ConstraintEqualTo(ContentView.LeftAnchor));
//			ContentView.AddConstraint(_mainStackView.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor));
//			ContentView.AddConstraint(_mainStackView.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor));
//			ContentView.AddConstraint(_mainStackView.RightAnchor.ConstraintEqualTo(ContentView.RightAnchor));
//
//			var highlightView = new StackedView (new CGSize (width: 8, height: 50)) { BackgroundColor = UIColor.Red } ;
//			var mainContentView = new StackedView (new CGSize (width: 500, height: 50)) { BackgroundColor = UIColor.Blue } ;
//			_mainStackView.AddArrangedSubview (highlightView);
//			_mainStackView.AddArrangedSubview (mainContentView);

		}

		public void SetSession(Session session)
		{
			_session = session;
		}

	}

	public class StackedView : UIView
	{
		CGSize size;

		public StackedView (CGSize size) : base ()
		{
			this.size = size;
			
		}

		public override CGSize IntrinsicContentSize {
			get {
				return this.size;
			}
		}
	}
}
