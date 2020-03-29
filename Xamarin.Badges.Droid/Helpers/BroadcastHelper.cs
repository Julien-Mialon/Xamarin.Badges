using System;
using System.Collections.Generic;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace Xamarin.Badges.Droid.Helpers
{
	internal static class BroadcastHelper
	{
		public static void SendIntentExplicitly(Context context, Intent intent)
		{
			IList<ResolveInfo> resolveInfos = ResolveBroadcast(context, intent);

			if (resolveInfos.Count == 0)
			{
				throw new ArgumentOutOfRangeException($"Unable to resolve intent {intent}");
			}
			
			foreach (ResolveInfo resolveInfo in resolveInfos)
			{
				if (resolveInfo != null)
				{
					Intent testIntent = new Intent(intent);
					testIntent.SetPackage(resolveInfo.ResolvePackageName);
					context.SendBroadcast(testIntent);
				}
			}
		}
		
		public static void SendDefaultIntentExplicitly(Context context, Intent intent)
		{
			if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
			{
				Intent oreoIntent = new Intent(intent);
				oreoIntent.SetAction(Constants.DEFAULT_OREO_INTENT_ACTION);

				try
				{
					SendIntentExplicitly(context, oreoIntent);
					return;
				}
				catch (ArgumentOutOfRangeException)
				{
					// ignore exception, try without Oreo code
				}
			}

			//Pre-oreo default method
			SendIntentExplicitly(context, intent);
		}

		public static IList<ResolveInfo> ResolveBroadcast(Context context, Intent intent)
		{
			PackageManager packageManager = context.PackageManager;
			IList<ResolveInfo> receivers = packageManager.QueryBroadcastReceivers(intent, PackageInfoFlags.MatchAll);
			return receivers ?? new List<ResolveInfo>();
		}
		
		public static bool CanResolveBroadcast(Context context, Intent intent)
		{
			return ResolveBroadcast(context, intent).Count > 0;
		}

		public static bool SendBroadcast(Context context, Intent intent)
		{
			try
			{
				SendIntentExplicitly(context, intent);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		
		public static bool SendDefaultBroadcast(Context context, Intent intent)
		{
			try
			{
				SendDefaultIntentExplicitly(context, intent);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}