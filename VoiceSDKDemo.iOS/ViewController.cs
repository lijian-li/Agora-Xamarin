using System;

using Foundation;
using UIKit;

using VoiceSDK.iOS;
using System.Threading.Tasks;

namespace VoiceSDKDemo.iOS {

	public partial class ViewController : UIViewController {

		const string VenderKey = "6D7A26A1D3554A54A9F43BE6797FE3E2";

		AgoraAudioKit _agoraAudioKit;
		bool _muteOn;
		bool _speakerOn;
		string _log;

		public ViewController(IntPtr handle) : base(handle) {
		}

		#region Lifecycle members
		public override async void ViewDidLoad() {
			base.ViewDidLoad();

			_agoraAudioKit = new AgoraAudioKit(OnQuality, OnError);

			JoinedChannel(false);

			#region Non-official APIs
			AppendLog("Register listeners");
			_agoraAudioKit.RecapStatBlock(OnRecapStat);
			_agoraAudioKit.SpeakersReportBlock(OnSpeakersReport);
			_agoraAudioKit.LogEventBlock(OnLogEvent);
			_agoraAudioKit.UpdateSessionStatBlock(OnUpdateSessionStat);
			//_agoraAudioKit.LeaveChannelBlock(OnLeaveChannel);
			//_agoraAudioKit
			//_agoraAudioKit

			AppendLog("Start echo test");
			_agoraAudioKit.StartEchoTest(VenderKey);
			await Task.Delay(2000);
			AppendLog("Stop echo test");
			_agoraAudioKit.StopEchoTest();

			AppendLog("Start network test");
			_agoraAudioKit.StartNetworkTest(VenderKey, OnNetworkTest);
			await Task.Delay(2000);
			AppendLog("Stop network test");
			_agoraAudioKit.StopNetworkTest();
			#endregion

			NameTextField.ShouldReturn += (textField) => { 
				textField.ResignFirstResponder();
				Join(null);
				return true; 
			};
		}
		#endregion

		#region Actions
		partial void Join(NSObject sender) {
			var channel = NameTextField.Text;
			if (!String.IsNullOrEmpty(channel)) {
				_agoraAudioKit.JoinChannelByKey(VenderKey, channel, "iPhone", 0, OnJoinSuccess);
			}
			else {
				AppendLog("Error: Channel name is empty.");
			}
		}

		partial void Leave(NSObject sender) {
			AppendLog("Leaving channel.");
			_agoraAudioKit.LeaveChannel();
			JoinedChannel(false);
		}

		partial void SwitchMute(NSObject sender) {
			_muteOn = !_muteOn;
			AppendLog(string.Format("Switching mute to {0}", _muteOn ? "ON" : "OFF"));
			_agoraAudioKit.SetMute(_muteOn);
			MuteSwitchButton.SetTitle(_muteOn ? "unmute" : "mute", UIControlState.Normal);
		}

		partial void SwitchSpeaker(NSObject sender) {
			_speakerOn = !_speakerOn;
			AppendLog(string.Format("Switching speaker to {0}", _speakerOn ? "ON" : "OFF"));
			_agoraAudioKit.SetEnableSpeaker(_speakerOn);
			SpeakerSwitchButton.SetTitle(_speakerOn ? "earpiece" : "speaker", UIControlState.Normal);
		}
		#endregion

		#region Callbacks from Agora Voice SDK
		void OnQuality(nuint uid, nuint delay, nuint jitter, nuint lost, nuint lost2) {
			AppendLog(string.Format("User {0} delay {1} jitter {2} lost {3} lost2 {4}", uid, delay, jitter, lost, lost2));
		}

		void OnError(AgoraAudioErrorCode error) {
			AppendLog(string.Format("Error code {0}", error));
		}

		void OnJoinSuccess(nuint sid, nuint uid) {
			AppendLog(string.Format("Channel joined: sid {0} uid {1}", sid, uid));
			JoinedChannel(true);
		}

		void OnRecapStat(sbyte recap, nint length) {
			AppendLog(string.Format("Recap stat: recap {0}, length {1}", recap, length));
		}

		void OnSpeakersReport(NSArray p0, nint p1) {
			AppendLog(string.Format("Speaker report: p0 {0}, p1 {1}", p0, p1));
		}

		void OnLogEvent(NSString log) {
			AppendLog(string.Format("Log event: {0}", log));
		}

		void OnUpdateSessionStat(AgoraAudioSessionStat stat) {
			AppendLog(string.Format("Leave channel session state: duration {0}, tx {1}, rx {2}, desc {3}, proxy: {4}, retain count {5}, zone {6}",
				stat.Duration, stat.TxBytes, stat.RxBytes, stat.Description, stat.IsProxy, stat.RetainCount, stat.Zone));
		}

		/*void OnLeaveChannel(AgoraAudioSessionStat stat) {
			AppendLog(string.Format("Leave channel session state: duration {0}, tx {1}, rx {2}, desc {3}, proxy: {4}, retain count {5}, zone {6}",
				stat.Duration, stat.TxBytes, stat.RxBytes, stat.Description, stat.IsProxy, stat.RetainCount, stat.Zone));
		}*/

		void OnNetworkTest(AgoraAudioMediaQuality mediaQuality) {
			AppendLog(string.Format("Network test: media quality {0}", mediaQuality));
		}
		#endregion

		#region Private members
		void AppendLog(string log) {
			InvokeOnMainThread(() => {
				Console.WriteLine(log);
				_log = String.Format("{0}{1} {2}\n", _log, DateTime.Now.ToLongTimeString(), log);
				LogMessageTextView.Text = _log;
				LogMessageTextView.ScrollRangeToVisible(new NSRange(_log.Length, 0));
			});
		}

		void JoinedChannel(bool joined) {
			JoinButton.Hidden = joined;
			LeaveButton.Hidden = !joined;
			MuteSwitchButton.Hidden = !joined;
			SpeakerSwitchButton.Hidden = !joined;
			NameTextField.Enabled = !joined;

			if (!joined) {
				_muteOn = false;
				_agoraAudioKit.SetMute(_muteOn);
				_speakerOn = false;
				_agoraAudioKit.SetEnableSpeaker(_speakerOn);
				_log = "";
				AppendLog(string.Format("Agora Voice SDK Demo for Xamarin.iOS\nSDK version {0}\n", AgoraAudioKit.SdkVersion));
			}
		}
		#endregion
	}
}
