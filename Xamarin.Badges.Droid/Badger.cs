using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using Xamarin.Badges.Droid.Implementations;
using Xamarin.Badges.Droid.Interfaces;

namespace Xamarin.Badges.Droid
{
	internal static class Badger
	{
		private static readonly List<IBadger> Badgers;
		private static IBadger _shortcutBadger;
		private static ComponentName _componentName;

		static Badger()
		{
			Badgers = new List<IBadger>
			{
				new AdwHomeBadger(),
				new ApexHomeBadger(),
				new AsusHomeBadger(),
				new HtcHomeBadger(),
				new HuaweiHomeBadger(),
				new LGHomeBadger(),
				new NovaBadger(),
				new OppoHomeBadger(),
				new SamsungHomeBadger(),
				new SonyHomeBadger(),
				new XiaomiHomeBadger(),
				new ZukHomeBadger()
			};
		}

		public static bool ApplyBadgeCount(Context context, int badgeCount)
		{
			try
			{
				if (_shortcutBadger == null)
				{
					if (!InitBadger(context))
					{
						return false;
					}
				}

				_shortcutBadger?.Set(context, _componentName, badgeCount);
				return true;
			}
			catch (Exception ex)
			{
				Log.Error("Badger", $"Error while trying to set a badge: {ex}");
				return false;
			}
		}

		private static bool InitBadger(Context context)
		{
			Intent launchIntent = context.PackageManager.GetLaunchIntentForPackage(context.PackageName);
			if (launchIntent == null)
			{
				Log.Error("Badger", "Unable to find launch intent for package " + context.PackageName);
				return false;
			}

			_componentName = launchIntent.Component;

			Intent intent = new Intent(Intent.ActionMain);
			intent.AddCategory(Intent.CategoryHome);
			ResolveInfo resolveInfo = context.PackageManager.ResolveActivity(intent, PackageInfoFlags.MatchDefaultOnly);

			if (resolveInfo == null || resolveInfo.ActivityInfo.Name.ToLowerInvariant().Contains("resolver"))
			{
				return false;
			}

			string currentHomePackage = resolveInfo.ActivityInfo.PackageName;

			foreach (IBadger badger in Badgers)
			{
				if (badger.SupportedLaunchers.Contains(currentHomePackage))
				{
					_shortcutBadger = badger;
					break;
				}
			}
			
			if (_shortcutBadger == null)
			{
				if (Build.Manufacturer.Equals("Xiaomi", StringComparison.InvariantCultureIgnoreCase))
				{
					_shortcutBadger = new XiaomiHomeBadger();
				}
				else if (Build.Manufacturer.Equals("ZUK", StringComparison.InvariantCultureIgnoreCase))
				{
					_shortcutBadger = new ZukHomeBadger();
				}
				if (Build.Manufacturer.Equals("OPPO", StringComparison.InvariantCultureIgnoreCase))
				{
					_shortcutBadger = new OppoHomeBadger();
				}
				else
				{
					_shortcutBadger = new DefaultBadger();
				}
			}

			return true;
		}
	}
}