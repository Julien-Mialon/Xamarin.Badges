using Android.Content;
using Android.Net;
using Android.OS;
using Xamarin.Badges.Droid.Interfaces;

namespace Xamarin.Badges.Droid.Implementations
{
	internal class ZukHomeBadger : IBadger
	{
		private static readonly Uri ContentUri = Uri.Parse("content://com.android.badge/badge");

		public bool Set(Context context, ComponentName component, int badgeCount)
		{
			Bundle extra = new Bundle();
			extra.PutInt("app_badge_count", badgeCount);
			context.ContentResolver.Call(ContentUri, "setAppBadgeCount", null, extra);
			return true;
		}

		public string[] SupportedLaunchers => new[]
		{
			"com.zui.launcher"
		};
	}
}