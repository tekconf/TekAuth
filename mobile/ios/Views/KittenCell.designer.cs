// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Collections.Touch
{
    [Register ("KittenCell")]
    partial class KittenCell
    {
        [Outlet]
        UIKit.UILabel MainLabel { get; set; }


        [Outlet]
        UIKit.UIImageView KittenImageView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (KittenImageView != null) {
                KittenImageView.Dispose ();
                KittenImageView = null;
            }

            if (MainLabel != null) {
                MainLabel.Dispose ();
                MainLabel = null;
            }
        }
    }
}