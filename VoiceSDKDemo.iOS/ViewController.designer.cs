// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace VoiceSDKDemo.iOS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIButton JoinButton { get; set; }

		[Outlet]
		UIKit.UILabel KeyLabel { get; set; }

		[Outlet]
		UIKit.UIButton LeaveButton { get; set; }

		[Outlet]
		UIKit.UITextView LogMessageTextView { get; set; }

		[Outlet]
		UIKit.UIButton MuteSwitchButton { get; set; }

		[Outlet]
		UIKit.UITextField NameTextField { get; set; }

		[Outlet]
		UIKit.UIButton SpeakerSwitchButton { get; set; }

		[Action ("Join:")]
		partial void Join (Foundation.NSObject sender);

		[Action ("Leave:")]
		partial void Leave (Foundation.NSObject sender);

		[Action ("SwitchMute:")]
		partial void SwitchMute (Foundation.NSObject sender);

		[Action ("SwitchSpeaker:")]
		partial void SwitchSpeaker (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (JoinButton != null) {
				JoinButton.Dispose ();
				JoinButton = null;
			}

			if (KeyLabel != null) {
				KeyLabel.Dispose ();
				KeyLabel = null;
			}

			if (LeaveButton != null) {
				LeaveButton.Dispose ();
				LeaveButton = null;
			}

			if (LogMessageTextView != null) {
				LogMessageTextView.Dispose ();
				LogMessageTextView = null;
			}

			if (MuteSwitchButton != null) {
				MuteSwitchButton.Dispose ();
				MuteSwitchButton = null;
			}

			if (NameTextField != null) {
				NameTextField.Dispose ();
				NameTextField = null;
			}

			if (SpeakerSwitchButton != null) {
				SpeakerSwitchButton.Dispose ();
				SpeakerSwitchButton = null;
			}
		}
	}
}
