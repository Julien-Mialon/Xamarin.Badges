using Xamarin.Badges.Droid.Implementations.Base;

namespace Xamarin.Badges.Droid.Implementations
{
	internal class DefaultBadger : BaseIntentBadger
	{
		protected override string IntentName => "android.intent.action.BADGE_COUNT_UPDATE";
		protected override string PackageParameterName => "badge_count_package_name";
		protected override string ClassParameterName => "badge_count_class_name";
		protected override string CountParameterName => "badge_count";
		public override string[] SupportedLaunchers => new string[0];
	}
}