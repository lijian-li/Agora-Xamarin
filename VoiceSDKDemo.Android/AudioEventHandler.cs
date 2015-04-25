using System;

using Android.App;
using Android.Views;
using Android.Widget;

using IO.Agoravoice.Voiceengine;

namespace VoiceSDKDemo.Android {

	public class AudioEventHandler : Java.Lang.Object, IAudioEventHandler {

		Activity _activity;
		TextView _logView;

		public AudioEventHandler(Activity activity, TextView logView) {
			_activity = activity;
			_logView = logView;
		}

		void Notify2UIThread(string message) {
			_activity.RunOnUiThread(() => {
				_logView.Append(String.Format("{0} {1}\n", DateTime.Now.ToLongTimeString(), message));
				var scrollView = (ScrollView)_logView.Parent;
				scrollView.FullScroll(FocusSearchDirection.Down);
			});
		}

		#region IAudioEventHandler implementation
		public void OnLogEvent(int level, String message) {
			Notify2UIThread(message);
		}

		public void OnError(int arg0) {
			Notify2UIThread(string.Format("Agora Voice SDK report error: {0}", arg0));
		}

		public void OnJoinSuccess(int sid, int uid) {
			Notify2UIThread(string.Format("Channel joined: sid {0} uid {1}", sid & 0xFFFFFFFFL, uid & 0xFFFFFFFFL));
		}

		public void OnLeaveChannel(AudioEventHandlerSessionStats stats) {
			Notify2UIThread(string.Format("End of call: duration {0} secs, total {1} bytes", stats.TotalDuration, stats.TotalBytes));
		}

		public void OnUpdateSessionStats(AudioEventHandlerSessionStats stats) {
			Notify2UIThread(string.Format("Update: duration {0} secs, total {1} bytes", stats.TotalDuration, stats.TotalBytes));
		}

		public void OnLoadAudioEngineSuccess() {
			Notify2UIThread("Agora audio engine loaded and call started");
		}

		public void OnQuality(int uid, short delay, short jitter, short lost, short lost2) {
			String msg = String.Format("User {0} delay {1} jitter {2} lost {3} lost2 {4}", (uid & 0xFFFFFFFFL), delay, jitter, lost, lost2);
			Notify2UIThread(msg);
		}

		public void OnUserOffline(int uid) {
			Notify2UIThread(string.Format("User {0} is offline", uid & 0xFFFFFFFFL));
		}

		public void OnRecapStat(byte[] recap) {
			Notify2UIThread(string.Format("Recap stat: {0}", recap));
		}

		public void OnSpeakersReport(AudioEventHandlerSpeakerInfo[] speakers, int mixVolume) {
			if (speakers != null) {
				Notify2UIThread(string.Format("Speakers report: user {0}, volume {1}", speakers[0].Uid & 0xFFFFFFFFL, speakers[0].Volume));
			}
		}

		public void OnNetworkTestResult(int quality) {
			Notify2UIThread(string.Format("Network test result: {0}", quality));
		}
		#endregion
	}
}

