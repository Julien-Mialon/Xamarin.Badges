namespace Xamarin.Badges.Droid.Implementations
{
	internal class LGHomeBadger : DefaultBadger
	{
		public override string[] SupportedLaunchers => new string[]
		{
			"com.lge.launcher",
			"com.lge.launcher2"
		};
	}
}