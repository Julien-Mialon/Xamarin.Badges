using Android.Content;
using Xamarin.Badges.Droid.Helpers;
using Xamarin.Badges.Droid.Interfaces;

namespace Xamarin.Badges.Droid.Implementations
{
	internal class HtcHomeBadger : IBadger
	{
		private const string INTENT_UPDATE_SHORTCUT = "com.htc.launcher.action.UPDATE_SHORTCUT";
		private const string INTENT_SET_NOTIFICATION = "com.htc.launcher.action.SET_NOTIFICATION";
		private const string PACKAGENAME = "packagename";
		private const string COUNT = "count";
		private const string EXTRA_COMPONENT = "com.htc.launcher.extra.COMPONENT";
		private const string EXTRA_COUNT = "com.htc.launcher.extra.COUNT";

		public bool Set(Context context, ComponentName component, int badgeCount)
		{
			Intent intent1 = new Intent(INTENT_SET_NOTIFICATION);
			intent1.PutExtra(EXTRA_COMPONENT, component.FlattenToShortString());
			intent1.PutExtra(EXTRA_COUNT, badgeCount);

			Intent intent2 = new Intent(INTENT_UPDATE_SHORTCUT);
			intent2.PutExtra(PACKAGENAME, component.PackageName);
			intent2.PutExtra(COUNT, badgeCount);

			return BroadcastHelper.SendBroadcast(context, intent1, intent2);
		}

		public string[] SupportedLaunchers => new []
		{
			"com.htc.launcher"
		};
	}
}