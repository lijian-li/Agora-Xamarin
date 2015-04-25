using System;

using Foundation;
using ObjCRuntime;

namespace VoiceSDK.iOS {
	// The first step to creating a binding is to add your native library ("libNativeLibrary.a")
	// to the project by right-clicking (or Control-clicking) the folder containing this source
	// file and clicking "Add files..." and then simply select the native library (or libraries)
	// that you want to bind.
	//
	// When you do that, you'll notice that MonoDevelop generates a code-behind file for each
	// native library which will contain a [LinkWith] attribute. MonoDevelop auto-detects the
	// architectures that the native library supports and fills in that information for you,
	// however, it cannot auto-detect any Frameworks or other system libraries that the
	// native library may depend on, so you'll need to fill in that information yourself.
	//
	// Once you've done that, you're ready to move on to binding the API...
	//
	//
	// Here is where you'd define your API definition for the native Objective-C library.
	//
	// For example, to bind the following Objective-C class:
	//
	//     @interface Widget : NSObject {
	//     }
	//
	// The C# binding would look like this:
	//
	//     [BaseType (typeof (NSObject))]
	//     interface Widget {
	//     }
	//
	// To bind Objective-C properties, such as:
	//
	//     @property (nonatomic, readwrite, assign) CGPoint center;
	//
	// You would add a property definition in the C# interface like so:
	//
	//     [Export ("center")]
	//     CGPoint Center { get; set; }
	//
	// To bind an Objective-C method, such as:
	//
	//     -(void) doSomething:(NSObject *)object atIndex:(NSInteger)index;
	//
	// You would add a method definition to the C# interface like so:
	//
	//     [Export ("doSomething:atIndex:")]
	//     void DoSomething (NSObject object, int index);
	//
	// Objective-C "constructors" such as:
	//
	//     -(id)initWithElmo:(ElmoMuppet *)elmo;
	//
	// Can be bound as:
	//
	//     [Export ("initWithElmo:")]
	//     IntPtr Constructor (ElmoMuppet elmo);
	//
	// For more information, see http://docs.xamarin.com/ios/advanced_topics/binding_objective-c_libraries
	//


	// @interface AgoraAudioSessionStat : NSObject
	[BaseType(typeof(NSObject))]
	interface AgoraAudioSessionStat {
		// @property (assign, nonatomic) NSUInteger duration;
		[Export("duration", ArgumentSemantic.Assign)]
		nuint Duration { get; set; }

		// @property (assign, nonatomic) NSUInteger txBytes;
		[Export("txBytes", ArgumentSemantic.Assign)]
		nuint TxBytes { get; set; }

		// @property (assign, nonatomic) NSUInteger rxBytes;
		[Export("rxBytes", ArgumentSemantic.Assign)]
		nuint RxBytes { get; set; }
	}

	// @interface AgoraAudioSpeakerInfo : NSObject
	[BaseType(typeof(NSObject))]
	interface AgoraAudioSpeakerInfo {
		// @property (assign, nonatomic) NSUInteger uid;
		[Export("uid", ArgumentSemantic.Assign)]
		nuint Uid { get; set; }

		// @property (assign, nonatomic) NSUInteger volume;
		[Export("volume", ArgumentSemantic.Assign)]
		nuint Volume { get; set; }
	}

	// @interface AgoraAudioKit : NSObject
	[BaseType(typeof(NSObject))]
	interface AgoraAudioKit {
		// +(NSString *)getSdkVersion;
		[Static]
		[Export("getSdkVersion")]
		string SdkVersion { get; }

		// -(id)initWithQuality:(void (^)(NSUInteger, NSUInteger, NSUInteger, NSUInteger, NSUInteger))qualityBlock error:(void (^)(AgoraAudioErrorCode))errorBlock;
		[Export("initWithQuality:error:")]
		IntPtr Constructor(Action<nuint, nuint, nuint, nuint, nuint> qualityBlock, Action<AgoraAudioErrorCode> errorBlock);

		// -(void)recapStatBlock:(void (^)(const char *, NSInteger))recapStatBlock;
		[Export("recapStatBlock:")]
		unsafe void RecapStatBlock(Action<sbyte, nint> recapStatBlock);

		// -(void)speakersReportBlock:(void (^)(NSArray *, NSInteger))speakersReportBlock;
		[Export("speakersReportBlock:")]
		void SpeakersReportBlock(Action<NSArray, nint> speakersReportBlock);

		// -(void)logEventBlock:(void (^)(NSString *))logEventBlock;
		[Export("logEventBlock:")]
		void LogEventBlock(Action<NSString> logEventBlock);

		// -(void)updateSessionStatBlock:(void (^)(AgoraAudioSessionStat *))updateSessionBlock;
		[Export("updateSessionStatBlock:")]
		void UpdateSessionStatBlock(Action<AgoraAudioSessionStat> updateSessionBlock);

		// -(void)leaveChannelBlock:(void (^)(AgoraAudioSessionStat *))leaveChannelBlock;
		[Export("leaveChannelBlock:")]
		void LeaveChannelBlock(Action<AgoraAudioSessionStat> leaveChannelBlock);

		// -(void)joinChannelByKey:(NSString *)vendorKey channelName:(NSString *)channelName info:(NSString *)info uid:(NSUInteger)uid success:(void (^)(NSUInteger, NSUInteger))successBlock;
		[Export("joinChannelByKey:channelName:info:uid:success:")]
		void JoinChannelByKey(string vendorKey, string channelName, string info, nuint uid, Action<nuint, nuint> successBlock);

		// -(void)leaveChannel;
		[Export("leaveChannel")]
		void LeaveChannel();

		// -(void)startEchoTest:(NSString *)vendorKey;
		[Export("startEchoTest:")]
		void StartEchoTest(string vendorKey);

		// -(void)stopEchoTest;
		[Export("stopEchoTest")]
		void StopEchoTest();

		// -(void)startNetworkTest:(NSString *)vendorKey networkTest:(void (^)(AgoraAudioMediaQuality))networkTestBlock;
		[Export("startNetworkTest:networkTest:")]
		void StartNetworkTest(string vendorKey, Action<AgoraAudioMediaQuality> networkTestBlock);

		// -(void)stopNetworkTest;
		[Export("stopNetworkTest")]
		void StopNetworkTest();

		// -(void)connectAudioServers:(NSString *)vendorKey connInfo:(NSString *)connInfo success:(void (^)(NSUInteger, NSUInteger))successBlock;
		[Export("connectAudioServers:connInfo:success:")]
		void ConnectAudioServers(string vendorKey, string connInfo, Action<nuint, nuint> successBlock);

		// -(void)setMute:(BOOL)shouldMute;
		[Export("setMute:")]
		void SetMute(bool shouldMute);

		// -(void)setMutePeers:(BOOL)shouldMute;
		[Export("setMutePeers:")]
		void SetMutePeers(bool shouldMute);

		// -(void)setEnableSpeaker:(BOOL)enableSpeaker;
		[Export("setEnableSpeaker:")]
		void SetEnableSpeaker(bool enableSpeaker);

		// -(void)setSpeakerVolume:(NSUInteger)volume;
		[Export("setSpeakerVolume:")]
		void SetSpeakerVolume(nuint volume);

		// -(void)setParameters:(NSString *)options;
		[Export("setParameters:")]
		void SetParameters(string options);

		// -(void)enableSpeakersReport:(NSInteger)interval smoothFactor:(NSInteger)smoothFactor;
		[Export("enableSpeakersReport:smoothFactor:")]
		void EnableSpeakersReport(nint interval, nint smoothFactor);

		// -(void)enableRecapStat:(NSInteger)interval;
		[Export("enableRecapStat:")]
		void EnableRecapStat(nint interval);

		// -(void)startRecapPlay;
		[Export("startRecapPlay")]
		void StartRecapPlay();

		// -(void)startRecording:(NSString *)filePath;
		[Export("startRecording:")]
		void StartRecording(string filePath);

		// -(void)stopRecording;
		[Export("stopRecording")]
		void StopRecording();

		// -(void)setProfile:(NSString *)profile merge:(BOOL)merge;
		[Export("setProfile:merge:")]
		void SetProfile(string profile, bool merge);
	}
}
