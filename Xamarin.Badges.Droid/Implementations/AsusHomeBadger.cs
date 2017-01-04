using Android.Content;

namespace Xamarin.Badges.Droid.Implementations
{
	internal class AsusHomeBadger : DefaultBadger
	{
		protected override void AddExtraToIntent(Intent intent, Context context, ComponentName component, int badgeCount)
		{
			intent.PutExtra("badge_vip_count", 0);

			base.AddExtraToIntent(intent, context, component, badgeCount);
		}

		public override string[] SupportedLaunchers => new[]
		{
			"com.asus.launcher"
		};
	}
}