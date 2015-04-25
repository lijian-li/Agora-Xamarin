using ObjCRuntime;

namespace VoiceSDK.iOS {
	[Native]
	public enum AgoraAudioErrorCode : ulong {
		NoError = 0uL,
		GetAudioAddrFailed = 1uL,
		GetAudioAddrZeroAddr,
		LoginMediaFailed,
		LoadAudioEngineError,
		StartCallError,
		CreateChannelTimeout,
		CreateChannelFailed,
		GetAudioAddrTimeout,
		ConnectMediaTimeout,
		ConnectMediaFailed,
		LoginMediaTimeout,
		RegetAudioAddr
	}

	[Native]
	public enum AgoraAudioMediaQuality : ulong {
		Unknown = 0uL,
		Excellent = 1uL,
		Good = 2uL,
		Poor = 3uL,
		Bad = 4uL
	}
}
