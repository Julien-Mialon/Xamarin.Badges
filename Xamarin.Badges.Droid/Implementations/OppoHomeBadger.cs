using Android.Content;
using Android.Net;
using Android.OS;
using Xamarin.Badges.Droid.Implementations.Base;

namespace Xamarin.Badges.Droid.Implementations
{
	internal class OppoHomeBadger : BaseIntentBadger
	{
		public override bool Set(Context context, ComponentName component, int badgeCount)
		{
			if (base.Set(context, component, badgeCount))
			{
				return true;
			}
			try
			{
				Bundle extras = new Bundle();
				extras.PutInt("app_badge_count", badgeCount);
				context.ContentResolver.Call(Uri.Parse("content://com.android.badge/badge"), "setAppBadgeCount", null, extras);
				return true;
			}
			catch (System.Exception)
			{
				return false;
			}
		}

		protected override void AddExtraToIntent(Intent intent, Context context, ComponentName component, int badgeCount)
		{
			intent.PutExtra("upgradeNumber", badgeCount);
			base.AddExtraToIntent(intent, context, component, badgeCount);
		}

		protected override string IntentName => "com.oppo.unsettledevent";
		protected override string PackageParameterName => "pakeageName";
		protected override string ClassParameterName => null;
		protected override string CountParameterName => "number";

		public override string[] SupportedLaunchers => new[]
		{
			"com.oppo.launcher"
		};
	}
}