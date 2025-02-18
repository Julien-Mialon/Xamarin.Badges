namespace Xamarin.Badges.Implementations;

internal class LGHomeBadger : DefaultBadger
{
	public override string[] SupportedLaunchers => new string[]
	{
		"com.lge.launcher",
		"com.lge.launcher2"
	};
}