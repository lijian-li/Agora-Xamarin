using System;

using Java.Lang;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using Android.Provider;
using Android.Text.Method;
using Android.Util;
using Android.Views;
using Android.Widget;

using IO.Agoravoice.Voiceengine;

namespace VoiceSDKDemo.Android {

	sealed class AppState {
		public const string IDLE = "Join";
		public const string RUNNING = "Mute";
		public const string MUTED = "Unmute";

		public string State { set; get; }
	}

	[Activity(
		Label = "@string/AppName",
		Icon = "@drawable/icon",
		ScreenOrientation = ScreenOrientation.Portrait,
		ConfigurationChanges = ConfigChanges.KeyboardHidden | ConfigChanges.Orientation | ConfigChanges.ScreenSize,
		MainLauncher = true
	)]
	public class MainActivity : Activity, AudioManager.IOnAudioFocusChangeListener {

		static string LOG_TAG = typeof(MainActivity).Name;

		AppState _appState = new AppState();

		View _scrollView;
		View _identificationView;
		EditText _key;
		EditText _channelId;
		EditText _userId;
		Button _ctrlBtn;
		Button _switcherBtn;

		AudioManager _am;
		bool _speakerOn;

		AgoraAudio _agoraAudio;

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);

			_appState.State = AppState.IDLE;

			_scrollView = FindViewById<ScrollView>(Resource.Id.scrollView);
			_identificationView = FindViewById<RelativeLayout>(Resource.Id.identification_control);

			TextView channelStatus = FindViewById<TextView>(Resource.Id.channel_status);
			channelStatus.MovementMethod = ScrollingMovementMethod.Instance;
			channelStatus.Text = "Welcome to Agora SDK Demo!\n";

			_key = FindViewById<EditText>(Resource.Id.key);
			_channelId = FindViewById<EditText>(Resource.Id.channel_id);
			_userId = FindViewById<EditText>(Resource.Id.user_id);

			_ctrlBtn = FindViewById<Button>(Resource.Id.btn_ctrl);
			_ctrlBtn.Click += (object sender, EventArgs e) => {
				switch (_appState.State) {
					case AppState.IDLE:
						var key = _key.Text;
						var channel = _channelId.Text;
						if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(channel)) {
							Toast.MakeText(this, "Channel ID or key is empty!", ToastLength.Long).Show();
							return;
						}

						int userId;
						if (!int.TryParse(_userId.Text, out userId))
							userId = 0;

						Log.Debug(LOG_TAG, "Before join channel");
						if (RequestAudioFocus()) {
							var extraInfo = "extra info you pass to SDK";
							_agoraAudio.JoinChannel(key, channel, extraInfo, userId);
						}
						else {
							Toast.MakeText(this, "Audio focus is required to workaround an android platform issue", ToastLength.Long).Show();
							Log.Debug(LOG_TAG, "Failed to acquire audio focus");
							return;
						}
						Log.Debug (LOG_TAG, "After join channel");

						_appState.State = AppState.RUNNING;

						_identificationView.Visibility = ViewStates.Gone;
						_scrollView.Visibility = ViewStates.Visible;
						_switcherBtn.Visibility = ViewStates.Visible;
						break;
					case AppState.RUNNING:
						_agoraAudio.Mute(true);
						_appState.State = AppState.MUTED;
						break;
					case AppState.MUTED:
						_agoraAudio.Mute(false);
						_appState.State = AppState.RUNNING;
						break;
				}

				_ctrlBtn.Text = _appState.State;
			};

			FindViewById<Button>(Resource.Id.btn_leave).Click += (object sender, EventArgs e) => Quit();

			_switcherBtn = FindViewById<Button>(Resource.Id.btn_switcher);
			_switcherBtn.Click += (object sender, EventArgs e) => SwitchOutput();
			_speakerOn = false;

			Window.AddFlags(WindowManagerFlags.KeepScreenOn);

			_agoraAudio = new AgoraAudio(this, true, new AudioEventHandler(this, channelStatus));
		}

		protected override void OnDestroy() {
			base.OnDestroy();

			_appState.State = AppState.IDLE;
			Window.AddFlags(WindowManagerFlags.KeepScreenOn);
		}

		public override void OnBackPressed() {
			Quit();
		}

		void SwitchOutput() {
			if (_speakerOn) {
				_speakerOn = false;
				_switcherBtn.Text = "Speaker";
				Log.Info(LOG_TAG, "Set audio output to speaker");
			}
			else {
				_speakerOn = true;
				_switcherBtn.Text = "Earpiece";
				Log.Info(LOG_TAG, "Set audio output to earpiece");
			}

			_agoraAudio.SetSpeaker(_speakerOn);
		}

		void Quit() {
			if (_appState.State != AppState.IDLE) {
				Log.Debug(LOG_TAG, "Before leave channel");
				_agoraAudio.LeaveChannel();
				AbandonAudioFocus();
				Log.Debug(LOG_TAG, "After leave channel");
				_appState.State = AppState.IDLE;

				_identificationView.Visibility = ViewStates.Visible;
				_scrollView.Visibility = ViewStates.Gone;
				_switcherBtn.Visibility = ViewStates.Gone;

				_ctrlBtn.Text = _appState.State;
			}
			else {
				Finish();
			}
		}

		bool RequestAudioFocus() {
			_am = (AudioManager)GetSystemService(Context.AudioService);
			var result = _am.RequestAudioFocus(this, Stream.VoiceCall, AudioFocus.Gain);

			if (result == AudioFocusRequest.Granted) {
				_am.Mode = Mode.InCommunication;
				return true;
			}
			else if (result == AudioFocusRequest.Failed) {
				return false;
			}

			throw new IllegalAccessError("Trespass");
		}

		void AbandonAudioFocus() {
			_am = (AudioManager)GetSystemService(Context.AudioService);
			_am.AbandonAudioFocus(this);
		}

		static string GetDeviceID(Context context) {
			return Settings.Secure.GetString(context.ContentResolver, Settings.Secure.AndroidId);
		}

		#region IOnAudioFocusChangeListener implementation
		public void OnAudioFocusChange(AudioFocus focusChange) {
			if (focusChange == AudioFocus.Loss) {
				_am.AbandonAudioFocus(this);
			}
			else if (focusChange == AudioFocus.Gain) {
				_am.Mode = Mode.InCommunication;
			}
		}
		#endregion
	}
}
