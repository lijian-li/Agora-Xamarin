using ObjCRuntime;

[assembly: LinkWith(
	"libAgoraAudioKit.a",
	LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Arm64 | LinkTarget.Simulator | LinkTarget.Simulator64,
	SmartLink = true,
	ForceLoad = true,
	Frameworks="AudioToolbox AVFoundation CoreTelephony SystemConfiguration",
	IsCxx = true,
	LinkerFlags="-lc++"
)]
