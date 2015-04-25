using System.Reflection;
using System.Runtime.CompilerServices;
using Android.App;

// Information about this assembly is defined by the following attributes.
// Change them to the values specific to your project.

[assembly: AssemblyTitle ("VoiceSDKDemo.Android")]
[assembly: AssemblyDescription ("Xamarin.Android Demo App for Agora Voice SDK")]
[assembly: AssemblyConfiguration ("")]
[assembly: AssemblyCompany ("")]
[assembly: AssemblyProduct ("")]
[assembly: AssemblyCopyright ("Lijian Li")]
[assembly: AssemblyTrademark ("")]
[assembly: AssemblyCulture ("")]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.

[assembly: AssemblyVersion ("1.0.0")]

// The following attributes are used to specify the signing key for the assembly,
// if desired. See the Mono documentation for more information about signing.

//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]

// Permissions required by Agora Voice SDK
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.RECORD_AUDIO")]
[assembly: UsesPermission(Name = "android.permission.MODIFY_AUDIO_SETTINGS")]
[assembly: UsesPermission(Name = "android.permission.ACCESS_NETWORK_STATE")]
[assembly: UsesPermission(Name = "android.permission.READ_EXTERNAL_STORAGE")]
