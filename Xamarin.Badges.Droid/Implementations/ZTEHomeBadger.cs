using Android.Content;
using Android.Net;
using Android.OS;
using Xamarin.Badges.Droid.Interfaces;

namespace Xamarin.Badges.Droid.Implementations
{
	internal class ZTEHomeBadger : IBadger
	{
		private static readonly Uri ContentUri = Uri.Parse("content://com.android.launcher3.cornermark.unreadbadge");

		public bool Set(Context context, ComponentName component, int badgeCount)
		{
			Bundle extra = new Bundle();
			extra.PutInt("app_badge_count", badgeCount);
			extra.PutString("app_badge_component_name", component.FlattenToString());
			
			
			context.ContentResolver.Call(ContentUri, "setAppUnreadCount", null, extra);
			return true;
		}

		public string[] SupportedLaunchers => new string[0];
	}
}