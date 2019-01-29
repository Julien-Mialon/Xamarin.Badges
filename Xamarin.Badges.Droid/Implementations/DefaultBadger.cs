using Android.Content;
using Android.OS;
using Xamarin.Badges.Droid.Helpers;
using Xamarin.Badges.Droid.Implementations.Base;

namespace Xamarin.Badges.Droid.Implementations
{
	internal class DefaultBadger : BaseIntentBadger
	{
		protected override string IntentName => Constants.DEFAULT_INTENT_ACTION;
		protected override string PackageParameterName => "badge_count_package_name";
		protected override string ClassParameterName => "badge_count_class_name";
		protected override string CountParameterName => "badge_count";

		protected override bool UseDefault => true;

		public override string[] SupportedLaunchers => new string[]
		{
			"fr.neamar.kiss",
			"com.quaap.launchtime",
			"com.quaap.launchtime_official"
		};

		public bool IsSupported(Context context)
		{
			return BroadcastHelper.CanResolveBroadcast(context, new Intent(Constants.DEFAULT_INTENT_ACTION	))
			       || (Build.VERSION.SdkInt >= BuildVersionCodes.O
			           && BroadcastHelper.CanResolveBroadcast(context, new Intent(Constants.DEFAULT_OREO_INTENT_ACTION)));
		}
	}
}