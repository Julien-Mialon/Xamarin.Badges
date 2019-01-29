using Android.Content;
using Android.Net;
using Android.OS;
using Xamarin.Badges.Droid.Implementations.Base;
using Xamarin.Badges.Droid.Interfaces;

namespace Xamarin.Badges.Droid.Implementations
{
	internal class OppoHomeBadger : BaseIntentBadger
	{
		private const string PROVIDER_CONTENT_URI = "content://com.android.badge/badge";
		private const string INTENT_ACTION = "com.oppo.unsettledevent";
		private const string INTENT_EXTRA_PACKAGENAME = "pakeageName";
		private const string INTENT_EXTRA_BADGE_COUNT = "number";
		private const string INTENT_EXTRA_BADGE_UPGRADENUMBER = "upgradeNumber";
		private const string INTENT_EXTRA_BADGEUPGRADE_COUNT = "app_badge_count";
		private int _currentTotalCount = -1;
		
		public override bool Set(Context context, ComponentName component, int badgeCount)
		{
			if (badgeCount == _currentTotalCount)
			{
				return true;
			}

			_currentTotalCount = badgeCount;

			if (Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb)
			{
				try
				{
					Bundle extras = new Bundle();
					extras.PutInt(INTENT_EXTRA_BADGEUPGRADE_COUNT, badgeCount);
					context.ContentResolver.Call(Uri.Parse(PROVIDER_CONTENT_URI), "setAppBadgeCount", null, extras);
					return true;
				}
				catch (System.Exception)
				{
					return false;
				}
			}

			return base.Set(context, component, badgeCount == 0 ? -1 : badgeCount);
		}

		protected override void AddExtraToIntent(Intent intent, Context context, ComponentName component, int badgeCount)
		{
			intent.PutExtra(INTENT_EXTRA_BADGE_UPGRADENUMBER, badgeCount);
			base.AddExtraToIntent(intent, context, component, badgeCount);
		}

		protected override string IntentName => INTENT_ACTION;
		protected override string PackageParameterName => INTENT_EXTRA_PACKAGENAME;
		protected override string ClassParameterName => null;
		protected override string CountParameterName => INTENT_EXTRA_BADGE_COUNT;

		public override string[] SupportedLaunchers => new[]
		{
			"com.oppo.launcher"
		};
	}
}