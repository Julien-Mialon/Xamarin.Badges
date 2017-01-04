using Xamarin.Badges.Droid.Implementations.Base;

namespace Xamarin.Badges.Droid.Implementations
{
	internal class AdwHomeBadger : BaseIntentBadger
	{
		protected override string IntentName => "org.adw.launcher.counter.SEND";
		protected override string PackageParameterName => "PNAME";
		protected override string ClassParameterName => "CNAME";
		protected override string CountParameterName => "COUNT";

		public override string[] SupportedLaunchers => new[]
		{
			"org.adw.launcher",
			"org.adwfreak.launcher"
		};
	}
}