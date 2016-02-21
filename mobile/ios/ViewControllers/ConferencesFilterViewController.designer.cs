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

namespace ios
{
    [Register ("ConferencesFilterViewController")]
    partial class ConferencesFilterViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch myScheduleOnly { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton resetFilter { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton saveFilter { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (myScheduleOnly != null) {
                myScheduleOnly.Dispose ();
                myScheduleOnly = null;
            }

            if (resetFilter != null) {
                resetFilter.Dispose ();
                resetFilter = null;
            }

            if (saveFilter != null) {
                saveFilter.Dispose ();
                saveFilter = null;
            }
        }
    }
}