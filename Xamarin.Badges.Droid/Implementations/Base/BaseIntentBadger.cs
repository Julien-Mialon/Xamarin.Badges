using Android.Content;
using Xamarin.Badges.Droid.Helpers;
using Xamarin.Badges.Droid.Interfaces;

namespace Xamarin.Badges.Droid.Implementations.Base
{
	internal abstract class BaseIntentBadger : IBadger
	{
		public virtual bool Set(Context context, ComponentName component, int badgeCount)
		{
			Intent intent = new Intent(IntentName);
			AddExtra(intent, PackageParameterName, component.PackageName);
			AddExtra(intent, ClassParameterName, component.ClassName);
			AddExtra(intent, CountParameterName, badgeCount);
			AddExtraToIntent(intent, context, component, badgeCount);

			return BroadcastHelper.SendBroadcast(context, intent);
		}

		protected virtual void AddExtraToIntent(Intent intent, Context context, ComponentName component, int badgeCount)
		{
			
		}

		protected void AddExtra(Intent intent, string key, string value)
		{
			if (string.IsNullOrEmpty(key))
			{
				return;
			}
			intent.PutExtra(key, value);
		}

		protected void AddExtra(Intent intent, string key, int value)
		{
			if (string.IsNullOrEmpty(key))
			{
				return;
			}
			intent.PutExtra(key, value);
		}

		protected abstract string IntentName { get; }
		protected abstract string PackageParameterName { get; }
		protected abstract string ClassParameterName { get; }
		protected abstract string CountParameterName { get; }

		public abstract string[] SupportedLaunchers { get; }
	}
}