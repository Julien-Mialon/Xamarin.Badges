using Android.Content;
using Android.Net;
using Android.OS;
using Xamarin.Badges.Droid.Interfaces;

namespace Xamarin.Badges.Droid.Implementations
{
	internal class HuaweiHomeBadger : IBadger
	{
		public bool Set(Context context, ComponentName component, int badgeCount)
		{
			Bundle localBundle = new Bundle();
			localBundle.PutString("package", component.PackageName);
			localBundle.PutString("class", component.ClassName);
			localBundle.PutInt("badgenumber", badgeCount);
			context.ContentResolver.Call(Uri.Parse("content://com.huawei.android.launcher.settings/badge/"), "change_badge", null, localBundle);
			//TODO: add better detection for this one
			return true;
		}

		public string[] SupportedLaunchers => new string[]
		{
			"com.huawei.android.launcher"
		};
	}
}