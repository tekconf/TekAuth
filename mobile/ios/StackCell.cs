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
		UIStackView _mainStackView;
		public StackCell (IntPtr handle) : base (handle)
		{
			_mainStackView = new UIStackView { 
				Axis = UILayoutConstraintAxis.Horizontal,
				Distribution = UIStackViewDistribution.FillProportionally,
				Alignment = UIStackViewAlignment.Leading,
				Spacing = 0
			};

			ContentView.Add (_mainStackView);

			var highlightView = new UIView { BackgroundColor = UIColor.Red };
			highlightView.TranslatesAutoresizingMaskIntoConstraints = false;
			//highlightView.AddConstraint (_mainStackView.LeftAnchor.ConstraintEqualTo (highlightView.LeftAnchor, 0));
			//highlightView.AddConstraint (_mainStackView.TopAnchor.ConstraintEqualTo (highlightView.TopAnchor, 0));
			//highlightView.AddConstraint (_mainStackView.BottomAnchor.ConstraintEqualTo (highlightView.BottomAnchor, 0));
			//highlightView.AddConstraint (highlightView.WidthAnchor.ConstraintEqualTo (8));

			_mainStackView.AddArrangedSubview (highlightView);

			_mainStackView.AddConstraint(highlightView.LeftAnchor.ConstraintEqualTo(_mainStackView.LeftAnchor));
			_mainStackView.AddConstraint(highlightView.TopAnchor.ConstraintEqualTo(_mainStackView.TopAnchor));
			_mainStackView.AddConstraint(highlightView.BottomAnchor.ConstraintEqualTo(_mainStackView.BottomAnchor));
			_mainStackView.AddConstraint(highlightView.WidthAnchor.ConstraintEqualTo(8));
		}

		public void SetSession(Session session)
		{
			_session = session;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
	

		}
	}
}
