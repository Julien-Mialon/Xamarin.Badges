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

		private static bool _initialized;
		private static readonly object Mutex = new object();

		static Badger()
		{
			Badgers = new List<IBadger>
			{
				new AdwHomeBadger(),
				new ApexHomeBadger(),
				new DefaultBadger(),
				new HtcHomeBadger(),
				new NovaBadger(),
				new SonyHomeBadger(),
				new AsusHomeBadger(),
				new HuaweiHomeBadger(),
				new OppoHomeBadger(),
				new SamsungHomeBadger(),
				new ZukHomeBadger(),
				new VivoHomeBadger(),
				new ZTEHomeBadger(),
				new EverythingMeHomeBadger(),
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
			if (_initialized)
			{
				return _shortcutBadger != null;
			}

			lock (Mutex)
			{
				if (_initialized)
				{
					return _shortcutBadger != null;
				}
				_initialized = true;

				Intent launchIntent = context.PackageManager.GetLaunchIntentForPackage(context.PackageName);
				if (launchIntent == null)
				{
					Log.Error("Badger", "Unable to find launch intent for package " + context.PackageName);
					return false;
				}

				_componentName = launchIntent.Component;

				Intent intent = new Intent(Intent.ActionMain);
				intent.AddCategory(Intent.CategoryHome);
				var resolveInfos = context.PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);

				foreach (ResolveInfo resolveInfo in resolveInfos)
				{
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
						if (Build.Manufacturer.Equals("ZUK", StringComparison.InvariantCultureIgnoreCase))
						{
							_shortcutBadger = new ZukHomeBadger();
						}
						else if (Build.Manufacturer.Equals("OPPO", StringComparison.InvariantCultureIgnoreCase))
						{
							_shortcutBadger = new OppoHomeBadger();
						}
						else if (Build.Manufacturer.Equals("VIVO", StringComparison.InvariantCultureIgnoreCase))
						{
							_shortcutBadger = new VivoHomeBadger();
						}
						else if (Build.Manufacturer.Equals("ZTE", StringComparison.InvariantCultureIgnoreCase))
						{
							_shortcutBadger = new ZTEHomeBadger();
						}
						if (Build.Manufacturer.Equals("Xiaomi", StringComparison.InvariantCultureIgnoreCase))
						{
							_shortcutBadger = new XiaomiHomeBadger();
						}
						else
						{
							_shortcutBadger = new DefaultBadger();
						}
					}
				}

				return true;
			}
		}
	}
}