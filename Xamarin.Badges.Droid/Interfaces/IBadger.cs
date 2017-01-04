using Android.Content;

namespace Xamarin.Badges.Droid.Interfaces
{
	internal interface IBadger
	{
		bool Set(Context context, ComponentName component, int badgeCount);

		string[] SupportedLaunchers { get; }
	}
}