using Xamarin.Badges.Implementations.Base;

namespace Xamarin.Badges.Implementations;

internal class ApexHomeBadger : BaseIntentBadger
{
	protected override string IntentName => "com.anddoes.launcher.COUNTER_CHANGED";
	protected override string PackageParameterName => "package";
	protected override string ClassParameterName => "class";
	protected override string CountParameterName => "count";

	public override string[] SupportedLaunchers => new[]
	{
		"com.anddoes.launcher"
	};
}