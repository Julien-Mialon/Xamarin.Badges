using Android.Content;
using Android.Net;
using Xamarin.Badges.Droid.Interfaces;

namespace Xamarin.Badges.Droid.Implementations
{
	internal class NovaBadger : IBadger
	{
		private const string CONTENT_URI = "content://com.teslacoilsw.notifier/unread_count";
		private const string COUNT = "count";
		private const string TAG = "tag";

		public bool Set(Context context, ComponentName component, int badgeCount)
		{
			ContentValues contentValues = new ContentValues();
			contentValues.Put(TAG, component.PackageName + "/" + component.ClassName);
			contentValues.Put(COUNT, badgeCount);
			context.ContentResolver.Insert(Uri.Parse(CONTENT_URI), contentValues);
			return true;
		}

		public string[] SupportedLaunchers => new[]
		{
			"com.teslacoilsw.launcher"
		};
	}
}