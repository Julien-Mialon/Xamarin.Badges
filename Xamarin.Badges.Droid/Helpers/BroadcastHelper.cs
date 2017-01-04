using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Content.PM;

namespace Xamarin.Badges.Droid.Helpers
{
	internal static class BroadcastHelper
	{
		public static bool SendBroadcast(Context context, params Intent[] intents)
		{
			if (intents.All(intent => CanResolveBroadcast(context, intent)))
			{
				foreach (Intent intent in intents)
				{
					context.SendBroadcast(intent);
				}
				return true;
			}
			return false;
		}

		public static bool CanResolveBroadcast(Context context, Intent intent)
		{
			PackageManager packageManager = context.PackageManager;
			IList<ResolveInfo> receivers = packageManager.QueryBroadcastReceivers(intent, PackageInfoFlags.MatchAll);
			return receivers != null && receivers.Count > 0;
		}
	}
}